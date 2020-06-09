using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace RemnantInspector.Classes
{
    public class CConfig
    {
        public int SaveSlotID { get; set; }
        public string GameplayMode { get; set; }
        public int HorizontalDisplayOffset { get; set; }
        public bool EnableBossTexts { get; set; }
        public bool EnableTraitTexts { get; set; }
        public bool EnableModTexts { get; set; }
        public bool EnableWeaponTexts { get; set; }
        public bool EnableArmorTexts { get; set; }
        public bool EnableAccessoryTexts { get; set; }
        public bool EnableEmoteTexts { get; set; }
        public bool EnableGameMonitor { get; set; }
        public bool ShowAppInTB { get; set; }

        public bool ReadConfig()
        {
            try
            {
                string appConfig = AppDomain.CurrentDomain.BaseDirectory;
                if (appConfig.Substring(appConfig.Length - 1, 1) != "\\")
                    appConfig += "\\";
                appConfig += "config.xml";
                StreamReader sInput = new StreamReader(appConfig);
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(sInput);

                XmlNode xLookup = xDoc.SelectSingleNode("RemnantInspector/RemnantConfig");
                if (xLookup == null)
                    throw new Exception("Missing node: RemnantConfig");
                
                SaveSlotID = 1;
                GameplayMode = "Default";
                if (xLookup["SaveSlot"] != null)
                {
                    if (int.TryParse(xLookup["SaveSlot"].InnerText, out int nSlot))
                        SaveSlotID = nSlot;
                }

                if (xLookup["GameplayMode"] != null)
                {
                    switch(xLookup["GameplayMode"].InnerText.ToUpper())
                    {
                        case "CAMPAIGN":
                        case "DEFAULT":
                            GameplayMode = "Campaign";
                            break;
                        case "ADVENTURE":
                            GameplayMode = "Adventure";
                            break;
                        default:
                            GameplayMode = "Campaign";
                            break;
                    }
                }

                HorizontalDisplayOffset = 32;
                xLookup = xDoc.SelectSingleNode("RemnantInspector/OverlaySettings");
                if(xLookup != null)
                {
                    if (xLookup["Offset_X"] != null)
                    {
                        if (int.TryParse(xLookup["Offset_X"].InnerText, out int nOffset))
                            HorizontalDisplayOffset = nOffset;
                    }

                    if (xLookup["EnableGameWatcher"] != null)                    
                        EnableGameMonitor = getBooleanFromString(xLookup["EnableGameWatcher"].InnerText);

                    if (xLookup["ShowAppInTaskbar"] != null)
                        ShowAppInTB = getBooleanFromString(xLookup["ShowAppInTaskbar"].InnerText);
                }

                xLookup = xDoc.SelectSingleNode("RemnantInspector/UISettings");
                if(xLookup != null)
                {
                    if (xLookup["EnableDescriptions_Boss"] != null)
                        EnableBossTexts = getBooleanFromString(xLookup["EnableDescriptions_Boss"].InnerText);
                    if (xLookup["EnableDescriptions_Trait"] != null)
                        EnableTraitTexts = getBooleanFromString(xLookup["EnableDescriptions_Trait"].InnerText);
                    if (xLookup["EnableDescriptions_Mod"] != null)
                        EnableModTexts = getBooleanFromString(xLookup["EnableDescriptions_Mod"].InnerText);
                    if (xLookup["EnableDescriptions_Weapon"] != null)
                        EnableWeaponTexts = getBooleanFromString(xLookup["EnableDescriptions_Weapon"].InnerText);
                    if (xLookup["EnableDescriptions_Armor"] != null)
                        EnableArmorTexts = getBooleanFromString(xLookup["EnableDescriptions_Armor"].InnerText);
                    if (xLookup["EnableDescriptions_Accessories"] != null)
                        EnableAccessoryTexts = getBooleanFromString(xLookup["EnableDescriptions_Accessories"].InnerText);
                    if (xLookup["EnableDescriptions_Emote"] != null)
                        EnableEmoteTexts = getBooleanFromString(xLookup["EnableDescriptions_Emote"].InnerText);
                }

                return true;
            }
            catch (Exception) { return false; }
        }

        private bool getBooleanFromString(string _input)
        {
            try
            {
                switch(_input.ToUpper())
                {
                    case "1":
                    case "TRUE":
                    case "YES":
                    case "ON":
                    case "ENABLED":
                        return true;
                    default:
                        return false;
                }
            }
            catch (Exception) { return false; }
        }
    }
}
