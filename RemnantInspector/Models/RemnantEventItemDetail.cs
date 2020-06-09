using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemnantInspector.Models
{
    public class RemnantEventItemDetail : INotifyPropertyChanged
    {
        private string itemPath, itemDisplayName, itemDescription, itemGroupIndex;
        private bool isMissingItem;

        public string ItemPath
        {
            get => itemPath;
            set { itemPath = value; NotifyProperty(); }
        }
        public string ItemDisplayName
        {
            get => itemDisplayName;
            set { itemDisplayName = value; NotifyProperty(); }
        }
        public string ItemDescription
        {
            get => itemDescription;
            set { itemDescription = value; NotifyProperty(); }
        }
        public string ItemGroupIndex
        {
            get => itemGroupIndex;
            set { itemGroupIndex = value; NotifyProperty(); }
        }
        public bool IsMissingItem
        {
            get => isMissingItem;
            set { isMissingItem = value; NotifyProperty(); }
        }

        #region Dynamic
        public string ItemType
        {
            get
            {
                if (ItemPath.Contains("Trait"))
                    return "Trait";
                else if (ItemPath.Contains("Mods"))
                    return "Mod";
                else if (ItemPath.Contains("Weapon"))
                    return "Weapon";
                else if (ItemPath.Contains("Armor"))
                    return "Armor";
                else if (ItemPath.Contains("Trinkets"))
                    return "Accessories";
                else if (ItemPath.Contains("Emote"))
                    return "Emote";
                else
                    return "Unknown";
            }
        }
        public string IsMissingItemDisplay
        {
            get { return IsMissingItem ? "NEW" : ""; }
        }
        public bool DisplayItemInfo
        {
            get
            {
                if (string.IsNullOrEmpty(ItemDescription))
                {
                    return false;
                }
                else
                {                    
                    if (IsMissingItem)
                    {
                        if ((ItemType == "Trait") && (App.config.EnableTraitTexts))
                            return true;
                        else if ((ItemType == "Mod") && (App.config.EnableModTexts))
                            return true;
                        else if ((ItemType == "Weapon") && (App.config.EnableWeaponTexts))
                            return true;
                        else if ((ItemType == "Armor") && (App.config.EnableArmorTexts))
                            return true;
                        else if ((ItemType == "Accessories") && (App.config.EnableAccessoryTexts))
                            return true;
                        else if ((ItemType == "Emote") && (App.config.EnableEmoteTexts))
                            return true;
                        else
                            return false;
                    }   
                    return false;
                }
            }
        }
        #endregion

        public RemnantEventItemDetail()
        {
            IsMissingItem = true;
            ItemGroupIndex = "1";
            ItemDescription = "";
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyProperty([CallerMemberName] string _prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prop));
        }
        #endregion
    }
}
