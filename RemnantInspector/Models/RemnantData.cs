using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RemnantInspector.Models.Enums;
using System.Collections.ObjectModel;

namespace RemnantInspector.Models
{
    public class RemnantData : INotifyPropertyChanged
    {
        private string eventTitle, internalName, eventLocation, eventMainZone, eventBossDescription;
        private RemnantEventTypes eventType;
        private ObservableCollection<RemnantEventItemDetail> eventItems;

        public string EventTitle
        {
            get => eventTitle;
            set { eventTitle = value; NotifyProperty(); }
        }
        public string InternalName
        {
            get => internalName;
            set { internalName = value; NotifyProperty(); }
        }
        public string EventLocation
        {
            get => eventLocation;
            set { eventLocation = value; NotifyProperty(); }
        }
        public string EventMainZone
        {
            get => eventMainZone;
            set { eventMainZone = value; NotifyProperty(); }
        }
        public string EventBossDescription
        {
            get => eventBossDescription;
            set { eventBossDescription = value; NotifyProperty(); }
        }
        public RemnantEventTypes EventType
        {
            get => eventType;
            set { eventType = value; NotifyProperty(); }
        }
        public ObservableCollection<RemnantEventItemDetail> EventItems
        {
            get => eventItems;
            set { eventItems = value; NotifyProperty(); }
        }

        #region Dynamic
        public bool IsAllItemsCompleted
        {
            get
            {
                bool bAllComplete = true;
                for(int n=0;n<EventItems.Count;n++)
                {
                    if(EventItems[n].IsMissingItem)
                    {
                        bAllComplete = false;
                        break;
                    }
                }
                return bAllComplete;
            }
        }
        public bool IsBossDescriptionAvailable
        {
            get 
            {
                if (string.IsNullOrEmpty(EventBossDescription))
                    return false;
                else
                {
                    if (App.config.EnableBossTexts)
                        return true;
                    else
                        return false;
                }
            }
        }
        public bool IsAdventureMode
        {
            get
            {
                if (App.config.GameplayMode == "Adventure")
                    return true;
                else
                    return false;
            }
        }
        public bool IsCampaignMode
        {
            get
            {
                if (App.config.GameplayMode == "Campaign")
                    return true;
                else
                    return false;
            }
        }
        #endregion

        public RemnantData()
        {
            EventItems = new ObservableCollection<RemnantEventItemDetail>();
            EventMainZone = "";
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyProperty([CallerMemberName] string _prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prop));
        }
        #endregion
    }
}
