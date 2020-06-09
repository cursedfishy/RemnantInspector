using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemnantInspector.Models
{
    public class SubLocation : INotifyPropertyChanged
    {
        private string encodedEventName, encodedLocation;

        public string EncodedEventName
        {
            get => encodedEventName;
            set { encodedEventName = value; NotifyProperty(); }
        }
        public string EncodedLocation
        {
            get => encodedLocation;
            set { encodedLocation = value; NotifyProperty(); }
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
