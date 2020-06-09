using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using RemnantInspector.Models;
using RemnantInspector.ViewModels;
using System.Windows.Interop;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Runtime.CompilerServices;

namespace RemnantInspector
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        #region legacy api
        public const int WS_EX_TRANSPARENT = 0x00000020;
        public const int GWL_EXSTYLE = (-20);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hwnd, int index);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);


        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int HOTKEY_ID_TOGGLE_DISPLAY = 9000;
        private const int HOTKEY_ID_CYCLE_UP = 9008;
        private const int HOTKEY_ID_CYCLE_DOWN = 9016;
        private const int HOTKEY_ID_CYCLE_LEFT = 9032;
        private const int HOTKEY_ID_CYCLE_RIGHT = 9064;

        //Modifiers:
        private const uint MOD_NONE = 0x0000;
        private const uint MOD_ALT = 0x0001;
        private const uint MOD_CONTROL = 0x0002;
        private const uint MOD_SHIFT = 0x0004;
        private const uint MOD_WIN = 0x0008;
        private const uint VK_CAPITAL = 0x14;
        private const uint VK_KEY_Q = 0x51;
        private const uint VK_UP = 0x26;
        private const uint VK_DOWN = 0x28;
        private const uint VK_LEFT = 0x25;
        private const uint VK_RIGHT = 0x27;

        private HwndSource source;

        #endregion

        private RemnantDataViewModel vm;
        private FileSystemWatcher fileMonitor;
        private SynchronizationContext mainThread;
        private int saveCount;
        private bool isRunning;
        private bool isOverlayVisible;
        private const double VERTICAL_SCROLL_FACTOR = 16f;

        public MainWindow()
        {
            InitializeComponent();
            SourceInitialized += MainWindow_SourceInitialized;
            Loaded += MainWindow_Loaded;
            Closing += MainWindow_Closing;
            SizeChanged += MainWindow_SizeChanged;
            mainThread = SynchronizationContext.Current;
            saveCount = 0;
            isOverlayVisible = true;

            string saveFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Remnant\\Saved\\SaveGames";
            fileMonitor = new FileSystemWatcher();
            fileMonitor.Path = saveFolder;

            fileMonitor.NotifyFilter = NotifyFilters.LastWrite;
            fileMonitor.Filter = "profile.sav";
            fileMonitor.EnableRaisingEvents = true;

            // Add event handlers.
            fileMonitor.Changed += FileMonitor_Changed;
            fileMonitor.Created += FileMonitor_Changed;
            fileMonitor.Deleted += FileMonitor_Changed;

            ShowInTaskbar = App.config.ShowAppInTB;
            if (App.config.EnableGameMonitor)
            {
                isRunning = true;
                Thread thWatcher = new Thread(() => gameWatcher());
                thWatcher.IsBackground = true;
                thWatcher.Start();
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            source.RemoveHook(HwndHook);

            UnregisterHotKey(windowHandle, HOTKEY_ID_TOGGLE_DISPLAY);
            if (App.config.GameplayMode == "Campaign")
            {
                UnregisterHotKey(windowHandle, HOTKEY_ID_CYCLE_UP);
                UnregisterHotKey(windowHandle, HOTKEY_ID_CYCLE_DOWN);
                UnregisterHotKey(windowHandle, HOTKEY_ID_CYCLE_LEFT);
                UnregisterHotKey(windowHandle, HOTKEY_ID_CYCLE_RIGHT);
            }
            base.OnClosed(e);
        }

        private void gameWatcher()
        {
            try
            {
                while (isRunning)
                {
                    if (!isRemnantRunning())
                        break;

                    Thread.Sleep(600);
                }
            }
            catch (Exception) { }
            mainThread.Send((object sender) =>
            {
                Application.Current.Shutdown(0);
            }, null);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var loc = lstData.PointFromScreen(new Point(0, 0));
            this.Left = App.config.HorizontalDisplayOffset;
            this.Top = (System.Windows.SystemParameters.WorkArea.Height / 2) - (e.NewSize.Height / 2);
            //Console.WriteLine(e.NewSize.Height.ToString());
        }

        private bool isRemnantRunning()
        {
            Process[] pname = Process.GetProcessesByName("Remnant");
            if (pname.Length == 0)
                return false;
            return true;
        }

        private void FileMonitor_Changed(object sender, FileSystemEventArgs e)
        {
            saveCount++;
            if (saveCount >= 4)
            {
                mainThread.Send((object senderAlt) => { vm.AnalyzeWorldSet(); }, null);
                saveCount = 0;
                //Console.WriteLine("!!!SAVE_UPDATE_TRIGGERED");
            }
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr windowHandle = new WindowInteropHelper(this).Handle;
            int extendedStyle = GetWindowLong(windowHandle, GWL_EXSTYLE);
            SetWindowLong(windowHandle, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);


            source = HwndSource.FromHwnd(windowHandle);
            source.AddHook(HwndHook);
            RegisterHotKey(windowHandle, HOTKEY_ID_TOGGLE_DISPLAY, MOD_CONTROL, VK_KEY_Q); // CTRL + Q Toggle Overlay Display

            if (App.config.GameplayMode == "Campaign")
            {
                RegisterHotKey(windowHandle, HOTKEY_ID_CYCLE_UP, 0, VK_UP);
                RegisterHotKey(windowHandle, HOTKEY_ID_CYCLE_DOWN, 0, VK_DOWN);
                RegisterHotKey(windowHandle, HOTKEY_ID_CYCLE_LEFT, 0, VK_LEFT);
                RegisterHotKey(windowHandle, HOTKEY_ID_CYCLE_RIGHT, 0, VK_RIGHT);
            }
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (App.config.GameplayMode == "Campaign")
            {
                const int WM_HOTKEY = 0x0312;
                switch (msg)
                {
                    case WM_HOTKEY:
                        switch (wParam.ToInt32())
                        {

                            case HOTKEY_ID_TOGGLE_DISPLAY:
                                {
                                    int vkey = (((int)lParam >> 16) & 0xFFFF);
                                    if (vkey == VK_KEY_Q)
                                    {
                                        isOverlayVisible = !isOverlayVisible;
                                        lstData.Visibility = (isOverlayVisible ? Visibility.Visible : Visibility.Hidden);
                                    }
                                    handled = true;
                                    break;
                                }
                            case HOTKEY_ID_CYCLE_UP:
                                {
                                    int vkey = (((int)lParam >> 16) & 0xFFFF);
                                    if (vkey == VK_UP)
                                    {
                                        double dblProjectedOffset = this.Top -= VERTICAL_SCROLL_FACTOR;
                                        double dblMinHeight = 0 - (this.Height / 2);
                                        if (dblProjectedOffset < dblMinHeight)
                                            dblProjectedOffset = dblMinHeight;
                                        this.Top = dblProjectedOffset;
                                    }                                    
                                    handled = true;
                                    break;
                                }
                            case HOTKEY_ID_CYCLE_DOWN:
                                {
                                    int vkey = (((int)lParam >> 16) & 0xFFFF);
                                    if (vkey == VK_DOWN)
                                    {
                                        double dblProjectedOffset = this.Top += VERTICAL_SCROLL_FACTOR;
                                        double dblMaxHeight = System.Windows.SystemParameters.WorkArea.Height - (this.Height / 2);
                                        if (dblProjectedOffset > dblMaxHeight)
                                            dblProjectedOffset = dblMaxHeight;
                                        this.Top = dblProjectedOffset;
                                    }
                                    handled = true;
                                    break;
                                }
                            case HOTKEY_ID_CYCLE_LEFT:
                                {
                                    
                                    int vkey = (((int)lParam >> 16) & 0xFFFF);
                                    if (vkey == VK_LEFT)
                                    {
                                        int nCurrentListIndex = 0;
                                        for (int n = 0; n < App.campaignDisplayList.Count; n++)
                                        {
                                            if (App.campaignDisplayList[n] == App.campaignDisplayGroup)
                                            {
                                                nCurrentListIndex = n;
                                                break;
                                            }
                                        }

                                        nCurrentListIndex--;
                                        if (nCurrentListIndex < 0)
                                            nCurrentListIndex = App.campaignDisplayList.Count - 1;
                                        App.campaignDisplayGroup = App.campaignDisplayList[nCurrentListIndex];
                                        Console.WriteLine("Current Group: " + App.campaignDisplayGroup);
                                        vm.TouchItems();
                                    }
                                    handled = true;
                                    break;
                                }
                            case HOTKEY_ID_CYCLE_RIGHT:
                                {
                                    int vkey = (((int)lParam >> 16) & 0xFFFF);
                                    if (vkey == VK_RIGHT)
                                    {
                                        int nCurrentListIndex = 0;
                                        for (int n = 0; n < App.campaignDisplayList.Count; n++)
                                        {
                                            if (App.campaignDisplayList[n] == App.campaignDisplayGroup)
                                            {
                                                nCurrentListIndex = n;
                                                break;
                                            }
                                        }

                                        nCurrentListIndex++;
                                        if (nCurrentListIndex > App.campaignDisplayList.Count - 1)
                                            nCurrentListIndex = 0;
                                        App.campaignDisplayGroup = App.campaignDisplayList[nCurrentListIndex];
                                        Console.WriteLine("Current Group: " + App.campaignDisplayGroup);
                                        vm.TouchItems();
                                    }                                    
                                    handled = true;
                                    break;
                                }
                        }
                        break;

                }
            }
            else
            {
                const int WM_HOTKEY = 0x0312;
                switch (msg)
                {
                    case WM_HOTKEY:
                        switch (wParam.ToInt32())
                        {

                            case HOTKEY_ID_TOGGLE_DISPLAY:
                                {
                                    int vkey = (((int)lParam >> 16) & 0xFFFF);
                                    if (vkey == VK_KEY_Q)
                                    {
                                        isOverlayVisible = !isOverlayVisible;
                                        lstData.Visibility = (isOverlayVisible ? Visibility.Visible : Visibility.Hidden);
                                    }
                                    handled = true;
                                    break;
                                }
                        }
                        break;
                }
            }
            return IntPtr.Zero;
        }


        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            vm = new RemnantDataViewModel();
            this.DataContext = vm;
            vm.ReadGameData();
            vm.AnalyzeWorldSet();
        }
    }
}
