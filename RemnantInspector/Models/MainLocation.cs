using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RemnantInspector.Models
{
    public class MainLocation : INotifyPropertyChanged
    {
        private string encodedKey, encodedLocation;

        public string EncodedKey
        {
            get => encodedKey;
            set { encodedKey = value; NotifyProperty(); }
        }
        public string EncodedLocation
        {
            get => encodedLocation;
            set { encodedLocation = value; NotifyProperty(); }
        }

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyProperty(string _prop="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_prop));
        }
        #endregion
    }
}
