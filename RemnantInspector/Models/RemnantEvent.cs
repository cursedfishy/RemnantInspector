using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemnantInspector.Models
{
    public class RemnantEvent : INotifyPropertyChanged
    {
        private string encodedEventName, encodedEventLocation;

        public string EncodedEventName
        {
            get => encodedEventName;
            set { encodedEventName = value; NotifyProperty(); }
        }
        public string EncodedEventLocation
        {
            get => encodedEventLocation;
            set { encodedEventLocation = value; NotifyProperty(); }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyProperty(string _property="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_property));
        }
        #endregion
    }
}
