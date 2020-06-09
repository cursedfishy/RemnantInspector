using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RemnantInspector.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using RemnantInspector.Models.Enums;
using System.Xml;

namespace RemnantInspector.ViewModels
{
    public class RemnantDataViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<RemnantData> remnantDataObjects;
        private List<MainLocation> mainLocations;
        private List<SubLocation> subLocations;
        private List<RemnantEventItem> eventData;
        private List<RemnantBossGuide> bossDescriptions;
        private List<RemnantEventItemDescription> itemDescriptions;
        private string adventureZone;

        public ObservableCollection<RemnantData> RemnantDataObjects
        {
            get => remnantDataObjects;
            set { remnantDataObjects = value; NotifyProperty(); }
        }
        public List<MainLocation> MainLocations
        {
            get => mainLocations;
            set { mainLocations = value; NotifyProperty(); }
        }
        public List<SubLocation> SubLocations
        {
            get => subLocations;
            set { subLocations = value; NotifyProperty(); }
        }
        public List<RemnantEventItem> EventData
        {
            get => eventData;
            set { eventData = value; NotifyProperty(); }
        }
        public List<RemnantBossGuide> BossDescriptions
        {
            get => bossDescriptions;
            set { bossDescriptions = value; NotifyProperty(); }
        }
        public List<RemnantEventItemDescription> ItemDescriptions
        {
            get => itemDescriptions;
            set { itemDescriptions = value; NotifyProperty(); }
        }
        public string AdventureZone
        {
            get => adventureZone;
            set { adventureZone = value; NotifyProperty(); }
        }

        public RemnantDataViewModel()
        {
            remnantDataObjects = new ObservableCollection<RemnantData>();
            MainLocations = new List<MainLocation>();
            SubLocations = new List<SubLocation>();
            EventData = new List<RemnantEventItem>();
            BossDescriptions = new List<RemnantBossGuide>();
            ItemDescriptions = new List<RemnantEventItemDescription>();
            AdventureZone = "Unknown";
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyProperty(string _prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prop));
        }
        #endregion

        public void TouchItems()
        {
            for (int n = 0; n < remnantDataObjects.Count; n++)
                remnantDataObjects[n].NotifyProperty("EventMainZone");

        }
        public bool ReadGameData()
        {
            MainLocations.Clear();
            SubLocations.Clear();
            EventData.Clear();
            BossDescriptions.Clear();
            ItemDescriptions.Clear();

            string appDir = AppDomain.CurrentDomain.BaseDirectory;
            if (appDir.Substring(appDir.Length - 1, 1) != "\\")
                appDir += "\\";

            if (!File.Exists(appDir + "GameInfo.db"))
                return false;

            XmlDocument xDoc = new XmlDocument();
            StreamReader sInput = new StreamReader(appDir + "GameInfo.db");
            xDoc.Load(sInput);

            XmlNodeList xNodes = xDoc.SelectNodes("RemnantDataSheet/MainLocations/MainLocation");
            foreach (XmlNode xLocation in xNodes)
            {
                if (xLocation.Attributes["key"] != null && xLocation.Attributes["name"] != null)
                    MainLocations.Add(new MainLocation()
                    {
                        EncodedKey = xLocation.Attributes["key"].InnerText,
                        EncodedLocation = xLocation.Attributes["name"].InnerText
                    });
            }

            xNodes = xDoc.SelectNodes("RemnantDataSheet/SubLocations/SubLocation");
            foreach (XmlNode xLocation in xNodes)
            {
                if (xLocation.Attributes["eventName"] != null && xLocation.Attributes["location"] != null)
                    SubLocations.Add(new SubLocation()
                    {
                        EncodedEventName = xLocation.Attributes["eventName"].InnerText,
                        EncodedLocation = xLocation.Attributes["location"].InnerText
                    });
            }

            xNodes = xDoc.SelectNodes("RemnantDataSheet/EventItems/Event");
            foreach (XmlNode xAllEvents in xNodes)
            {
                if (xAllEvents.Attributes["name"] != null)
                {
                    RemnantEventItem newItem = new RemnantEventItem();
                    newItem.EventIdentifier = xAllEvents.Attributes["name"].InnerText;
                    newItem.EventItemList = new ObservableCollection<RemnantEventItemDetail>();

                    XmlNodeList xSubItems = xAllEvents.SelectNodes("Item");
                    foreach (XmlNode xItem in xSubItems)
                    {
                        RemnantEventItemDetail detailItemData = new RemnantEventItemDetail();
                        if (xItem.Attributes["name"] != null)
                        {
                            if (!string.IsNullOrEmpty(xItem.Attributes["name"].InnerText))
                            {
                                detailItemData.ItemDisplayName = xItem.Attributes["name"].InnerText;
                                if (xItem.Attributes["index"] != null)
                                {
                                    if (!string.IsNullOrEmpty(xItem.Attributes["index"].InnerText))
                                    {
                                        detailItemData.ItemGroupIndex = xItem.Attributes["index"].InnerText;
                                    }
                                    else { detailItemData.ItemGroupIndex = "1"; }
                                }
                                else { detailItemData.ItemGroupIndex = "1"; }
                            }
                        }

                        detailItemData.ItemPath = xItem.InnerText;
                        if (string.IsNullOrEmpty(detailItemData.ItemDisplayName))
                        {
                            detailItemData.ItemDisplayName = getDisplayName(detailItemData.ItemPath);
                        }

                        newItem.EventItemList.Add(detailItemData);
                    }
                    if (newItem.EventItemList.Count >= 1)
                        EventData.Add(newItem);
                }
            }

            xNodes = xDoc.SelectNodes("RemnantDataSheet/BossDescriptions/Boss");
            foreach (XmlNode xBoss in xNodes)
            {
                if (xBoss.Attributes["name"] != null)
                {
                    if (!string.IsNullOrEmpty(xBoss.InnerText))
                    {
                        RemnantBossGuide bossData = new RemnantBossGuide();
                        bossData.eventName = xBoss.Attributes["name"].InnerText;
                        bossData.bossInfoText = xBoss.InnerText;
                        BossDescriptions.Add(bossData);
                    }
                }
            }

            xNodes = xDoc.SelectNodes("RemnantDataSheet/ItemDescriptions/ItemRoot");
            foreach (XmlNode xItemRoot in xNodes)
            {
                if (xItemRoot.Attributes["name"] != null)
                {
                    if (!string.IsNullOrEmpty(xItemRoot.Attributes["name"].InnerText))
                    {
                        RemnantEventItemDescription descriptionBlock = new RemnantEventItemDescription();
                        descriptionBlock.EventName = xItemRoot.Attributes["name"].InnerText;

                        XmlNodeList xGroupNodes = xItemRoot.SelectNodes("ItemGroup");
                        foreach (XmlNode xItemGroup in xGroupNodes)
                        {
                            if (xItemGroup.Attributes["index"] != null)
                            {
                                if (!string.IsNullOrEmpty(xItemGroup.Attributes["index"].InnerText))
                                {
                                    RemnantEventItemDescriptionBlock detailedDescBlock = new RemnantEventItemDescriptionBlock();
                                    detailedDescBlock.EventItemGroupIndex = xItemGroup.Attributes["index"].InnerText;
                                    detailedDescBlock.GroupIndexDescription = xItemGroup.InnerText;
                                    descriptionBlock.ItemDescriptionBlocks.Add(detailedDescBlock);
                                }
                            }
                        }

                        if (descriptionBlock.ItemDescriptionBlocks.Count >= 1)
                        {
                            if (!ItemDescriptions.Any(f => f.EventName == descriptionBlock.EventName))
                            {
                                ItemDescriptions.Add(descriptionBlock);
                            }
                        }
                    }
                }
            }

            // Assign Descriptions to appropiate slot
            for (int n = 0; n < ItemDescriptions.Count; n++)
            {
                for (int x = 0; x < EventData.Count; x++)
                {
                    if (EventData[x].EventIdentifier == ItemDescriptions[n].EventName)
                    {
                        for (int y = EventData[x].EventItemList.Count -1; y >= 0; y--)
                        {
                            EventData[x].EventItemList[y].ItemDescription = string.Empty;
                            for (int nx=0;nx< ItemDescriptions[n].ItemDescriptionBlocks.Count;nx++)
                            {
                                if((ItemDescriptions[n].ItemDescriptionBlocks[nx].EventItemGroupIndex == EventData[x].EventItemList[y].ItemGroupIndex) &&
                                    (ItemDescriptions[n].ItemDescriptionBlocks[nx].IsFree))
                                {
                                    EventData[x].EventItemList[y].ItemDescription = ItemDescriptions[n].ItemDescriptionBlocks[nx].GroupIndexDescription;
                                    ItemDescriptions[n].ItemDescriptionBlocks[nx].IsFree = false;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
        public void AnalyzeWorldSet()
        {
            // Clear 
            remnantDataObjects.Clear();
            App.campaignDisplayList.Clear();
            App.campaignDisplayGroup = "*";            

            string saveSlotName = "save_" + (App.config.SaveSlotID - 1).ToString() + ".sav";
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Remnant\\Saved\\SaveGames\\" + saveSlotName;
            string fileData = File.ReadAllText(filePath);

            if (App.config.GameplayMode == "Adventure")
            {
                AdventureZone = "Unknown Zone";

                if (fileData.Contains("Quest_AdventureMode_"))
                {
                    if (fileData.Contains("Quest_AdventureMode_City_C")) AdventureZone = "City";
                    if (fileData.Contains("Quest_AdventureMode_Wasteland_C")) AdventureZone = "Wasteland";
                    if (fileData.Contains("Quest_AdventureMode_Swamp_C")) AdventureZone = "Swamp";
                    if (fileData.Contains("Quest_AdventureMode_Jungle_C")) AdventureZone = "Jungle";

                    string adventureTextEnd = String.Format("/Game/World_{0}/Quests/Quest_AdventureMode/Quest_AdventureMode_{0}.Quest_AdventureMode_{0}_C", AdventureZone);
                    int adventureEnd = fileData.IndexOf(adventureTextEnd) + adventureTextEnd.Length;
                    string adventureText = fileData.Substring(0, adventureEnd);

                    string adventureTextStart = String.Format("/Game/World_{0}/Quests/Quest_AdventureMode/Quest_AdventureMode_{0}_0", AdventureZone);
                    int adventureStart = adventureText.LastIndexOf(adventureTextStart) + adventureTextStart.Length;
                    adventureText = adventureText.Substring(adventureStart);

                    parseAdventureText(adventureText);
                }
            }
            else
            {
                string campaignTextEnd = "/Game/Campaign_Main/Quest_Campaign_Main.Quest_Campaign_Main_C";
                string campaignTextStart = "/Game/Campaign_Main/Quest_Campaign_City.Quest_Campaign_City";
                int campaignEnd = fileData.IndexOf(campaignTextEnd);
                int campaignStart = fileData.IndexOf(campaignTextStart);
                if (campaignStart != -1 && campaignEnd != -1)
                {
                    string campaigntext = fileData.Substring(0, campaignEnd);
                    campaignStart = campaigntext.LastIndexOf(campaignTextStart);
                    campaigntext = campaigntext.Substring(campaignStart);                    
                    parseAdventureText(campaigntext);
                }
            }
        }
        private void parseAdventureText(string adventureText)
        {
            string zone = null;
            string currentMainLocation = "Fairview";
            string currentSublocation = null;
            string eventName = null;
            string lastEventname = eventName;

            MatchCollection matches = Regex.Matches(adventureText, "(?:/[a-zA-Z0-9_]+){3}/(([a-zA-Z0-9]+_[a-zA-Z0-9]+_[a-zA-Z0-9_]+)|Quest_Church)");
            foreach (Match match in matches)
            {
                RemnantEventTypes? eventType = null;
                eventName = null;
                string textLine = match.Value;

                try
                {
                    if (currentSublocation != null)
                    {
                        if (currentSublocation.Equals("TheRavagersHaunt") || currentSublocation.Equals("TheTempestCourt")) currentSublocation = null;
                    }
                    zone = getZone(textLine);
                    eventType = getEventType(textLine);

                    if (textLine.Contains("Overworld_Zone"))
                    {
                        //process overworld zone marker
                        currentMainLocation = textLine.Split('/')[4].Split('_')[1] + " " + textLine.Split('/')[4].Split('_')[2] + " " + textLine.Split('/')[4].Split('_')[3];
                        bool bMatch = false;
                        for (int n = 0; n < MainLocations.Count; n++)
                        {
                            if (MainLocations[n].EncodedKey == currentMainLocation)
                            {
                                currentMainLocation = MainLocations[n].EncodedLocation;
                                bMatch = true;
                                break;
                            }
                        }
                        if (!bMatch)
                            currentMainLocation = null;
                        continue;
                    }
                    else if (textLine.Contains("Quest_Church"))
                    {
                        //process Root Mother event
                        currentMainLocation = "Chapel Station";
                        eventName = "RootMother";
                        currentSublocation = "Church of the Harbinger";
                    }
                    else if (eventType != null)
                    {
                        eventName = textLine.Split('/')[4].Split('_')[2];
                        if (textLine.Contains("OverworldPOI") || textLine.Contains("Sketterling"))
                        {
                            currentSublocation = null;
                        }
                        else if (!textLine.Contains("Quest_Event"))
                        {
                            currentSublocation = null;
                            for (int n = 0; n < SubLocations.Count; n++)
                            {
                                if (SubLocations[n].EncodedEventName == eventName)
                                {
                                    currentSublocation = SubLocations[n].EncodedLocation;
                                    break;
                                }
                            }
                        }
                        if ("Chapel Station".Equals(currentMainLocation))
                        {
                            if (textLine.Contains("Quest_Boss"))
                            {
                                currentMainLocation = "Westcourt";
                            }
                            else
                            {
                                currentSublocation = null;
                            }
                        }
                    }
                    currentMainLocation = null;

                    // Pattern Replacement
                    if (eventName != lastEventname)
                    {
                        RemnantData dataObject = new RemnantData();
                        if (currentSublocation != null)
                            dataObject.EventLocation = zone + " → " + Regex.Replace(currentSublocation, "(\\B[A-Z])", " $1");
                        else
                            dataObject.EventLocation = zone;
                        dataObject.EventType = eventType.HasValue ? eventType.Value : RemnantEventTypes.UNKNOWN;
                        dataObject.EventMainZone = zone != null ? zone.ToString() : "";
                        if(!string.IsNullOrEmpty(dataObject.EventMainZone))
                        {
                            if (!App.campaignDisplayList.Contains(dataObject.EventMainZone))
                                App.campaignDisplayList.Add(dataObject.EventMainZone);
                        }

                        if (dataObject.EventType != RemnantEventTypes.UNKNOWN)
                        {
                            dataObject.InternalName = eventName;
                            // Replacements
                            if (eventName != null)
                            {
                                dataObject.EventTitle = eventName.Replace("LizAndLiz", "TaleOfTwoLiz's").Replace("Fatty", "TheUncleanOne").Replace("QueensTemple", "IskalQueen").Replace("WastelandGuardian", "Claviger").Replace("RootEnt", "TheEnt").Replace("Wolf", "TheRavager").Replace("RootDragon", "Singe").Replace("SwarmMaster", "Scourge").Replace("RootWraith", "Shroud").Replace("RootTumbleweed", "TheMangler").Replace("Kincaller", "Warden").Replace("Tyrant", "Thrall").Replace("Vyr", "ShadeAndShatter").Replace("ImmolatorAndZephyr", "ScaldAndSear").Replace("RootBrute", "Gorefist").Replace("SlimeHulk", "Canker").Replace("BlinkFiend", "Onslaught").Replace("Sentinel", "Raze").Replace("Penitent", "Leto'sAmulet").Replace("LastWill", "SupplyRun").Replace("SwampGuardian", "Ixillis").Replace("OldManAndConstruct", "WudAndAncientConstruct").Replace("Splitter", "Riphide").Replace("Nexus", "RootNexus").Replace("FlickeringHorror", "DreamEater").Replace("BarbTerror", "BarbedTerror").Replace("Wisp", "CircletHatchery").Replace("GunslignersRing", "Gunslinger'sRing");

                                dataObject.EventTitle = Regex.Replace(dataObject.EventTitle, "([a-z])([A-Z])", "$1 $2");
                            }

                            if (zone != null && eventType != null && eventName != null && eventType != RemnantEventTypes.UNKNOWN)
                            {
                                // rings drop with the Cryptolith on Rhom
                                if (eventName.Equals("Cryptolith") && zone.Equals("Rhom"))
                                {
                                    RemnantData dataObjectSpecial = new RemnantData();
                                    dataObjectSpecial.EventType = RemnantEventTypes.ITEMDROP;
                                    dataObjectSpecial.EventTitle = "Soul Link";
                                    dataObjectSpecial.EventLocation = currentSublocation;
                                    dataObjectSpecial.EventMainZone = currentMainLocation != null ? currentMainLocation.ToString() : "";
                                    RemnantDataObjects.Add(dataObjectSpecial);

                                }
                                // beetles spawn in Strange Pass
                                else if (eventName.Equals("BrainBug") || eventName.Equals("FlickeringHorror") || eventName.Equals("BarbTerror") || eventName.Equals("Wisp"))
                                {
                                    RemnantData dataObjectSpecial = new RemnantData();
                                    dataObjectSpecial.EventTitle = "Timid Beetle";
                                    dataObjectSpecial.EventLocation = currentSublocation;
                                    dataObjectSpecial.EventMainZone = currentMainLocation != null ? currentMainLocation.ToString() : "";
                                    dataObjectSpecial.EventType = RemnantEventTypes.EVENT;

                                    if (eventName.Equals("BrainBug"))
                                    {
                                        dataObjectSpecial.EventType = RemnantEventTypes.EVENT;
                                    }
                                    else
                                    {
                                        dataObjectSpecial.EventType = RemnantEventTypes.EVENTRNG;
                                    }
                                    RemnantDataObjects.Add(dataObjectSpecial);
                                }
                                lastEventname = eventName;
                            }

                            dataObject.EventBossDescription = getBossDescription(dataObject.InternalName);
                            dataObject.EventItems = getItemList(dataObject.InternalName);
                            RemnantDataObjects.Add(dataObject);
                        }
                    }
                }
                catch (Exception error) { Console.WriteLine(error.ToString()); }
            }

            App.campaignDisplayGroup = App.campaignDisplayList.First();

            // Check PlayerData from List
            CrossCheckPlayerProfile();
        }
        public void CrossCheckPlayerProfile()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Remnant\\Saved\\SaveGames\\profile.sav";
            string fileData = File.ReadAllText(filePath);
            string[] charData = fileData.Split(new string[] { "/Game/Characters/Player/Base/Character_Master_Player.Character_Master_Player_C" }, StringSplitOptions.None);

            int nSlotID = 1;
            for (var i = 1; i < charData.Length; i++)
            {
                Match archetypeMatch = new Regex(@"/Game/_Core/Archetypes/[a-zA-Z_]+").Match(charData[i - 1]);
                if (archetypeMatch.Success)
                {
                    if(nSlotID == App.config.SaveSlotID)
                    {
                        string classText = archetypeMatch.Value;
                        string charEnd = "Character_Master_Player_C";
                        string inventory = charData[i].Substring(0, charData[i].IndexOf(charEnd));

                        for (int n = 0; n < RemnantDataObjects.Count; n++)
                        {
                            for (int x = 0; x < RemnantDataObjects[n].EventItems.Count; x++)
                            {
                                if (inventory.Contains(RemnantDataObjects[n].EventItems[x].ItemPath))
                                    RemnantDataObjects[n].EventItems[x].IsMissingItem = false;
                            }
                        }
                    }
                    nSlotID++;
                }
            }

            /*
            for (int n = 0; n < RemnantDataObjects.Count; n++)
            {
                for (int x = 0; x < RemnantDataObjects[n].EventItems.Count; x++)
                {
                    if (fileData.Contains(RemnantDataObjects[n].EventItems[x].ItemPath))
                        RemnantDataObjects[n].EventItems[x].IsMissingItem = false;
                }
            }
            */
        }


        private ObservableCollection<RemnantEventItemDetail> getItemList(string _eventIdentifier)
        {
            ObservableCollection<RemnantEventItemDetail> result = new ObservableCollection<RemnantEventItemDetail>();
            for (int n = 0; n < EventData.Count; n++)
            {
                if (EventData[n].EventIdentifier == _eventIdentifier)
                {
                    for (int x = 0; x < EventData[n].EventItemList.Count; x++)
                        result.Add(EventData[n].EventItemList[x]);
                }
            }
            return result;
        }

        private string getDisplayName(string _dataInput)
        {
            if (_dataInput.Contains("/"))
            {
                return _dataInput.Substring(_dataInput.LastIndexOf('/') + 1);
            }
            else { return _dataInput; }
        }
        private string getBossDescription(string _eventIdentifier)
        {
            for (int n = 0; n < BossDescriptions.Count; n++)
            {
                if (BossDescriptions[n].eventName == _eventIdentifier)
                    return BossDescriptions[n].bossInfoText;
            }
            return string.Empty;
        }

        private string getZone(string textLine)
        {
            string zone = null;
            if (textLine.Contains("World_City") || textLine.Contains("Quest_Church"))
            {
                zone = "Earth";
            }
            else if (textLine.Contains("World_Wasteland"))
            {
                zone = "Rhom";
            }
            else if (textLine.Contains("World_Jungle"))
            {
                zone = "Yaesha";
            }
            else if (textLine.Contains("World_Swamp"))
            {
                zone = "Corsus";
            }
            return zone;
        }

        private RemnantEventTypes? getEventType(string textLine)
        {
            RemnantEventTypes? result = null;

            if (textLine.Contains("SmallD"))
            {
                result = RemnantEventTypes.DUNGEON;
            }
            else if (textLine.Contains("Quest_Boss"))
            {
                result = RemnantEventTypes.WORLDBOSS;
            }
            else if (textLine.Contains("Siege") || textLine.Contains("Quest_Church"))
            {
                result = RemnantEventTypes.SIEGE;
            }
            else if (textLine.Contains("Mini"))
            {
                result = RemnantEventTypes.MINIBOSS;
            }
            else if (textLine.Contains("Quest_Event"))
            {
                if (textLine.Contains("Nexus"))
                {
                    result = RemnantEventTypes.SIEGE;
                }
                else if (textLine.Contains("Sketterling"))
                {
                    result = RemnantEventTypes.EVENTQS;
                }
                else
                {
                    result = RemnantEventTypes.ITEMDROP;
                }
            }
            else if (textLine.Contains("OverworldPOI") || textLine.Contains("OverWorldPOI"))
            {
                result = RemnantEventTypes.POI;
            }
            return result;
        }

    }
}
