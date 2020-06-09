using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace RemnantInspector.Models
{
    public class RemnantEventItem : INotifyPropertyChanged
    {
        private string eventIdentifier;
        private ObservableCollection<RemnantEventItemDetail> eventItemList;

        public string EventIdentifier
        {
            get => eventIdentifier;
            set { eventIdentifier = value; NotifyProperty(); }
        }
        public ObservableCollection<RemnantEventItemDetail> EventItemList
        {
            get => eventItemList;
            set { eventItemList = value; NotifyProperty(); }
        }

        public RemnantEventItem()
        {
            EventItemList = new ObservableCollection<RemnantEventItemDetail>();
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
