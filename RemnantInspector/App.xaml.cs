using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using RemnantInspector.Classes;

namespace RemnantInspector
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static CConfig config;
        public static string campaignDisplayGroup = "*";        
        public static List<string> campaignDisplayList;

        public App()
        {
            AppDomain current = AppDomain.CurrentDomain;
            current.UnhandledException += Current_UnhandledException;
            
            campaignDisplayList = new List<string>();
            config = new CConfig();
            if(!config.ReadConfig())
            {
                MessageBox.Show("Error loading config.xml", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(0);
            }
        }

        private void Current_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception err = (Exception)e.ExceptionObject;
            MessageBox.Show(err.ToString(), "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
