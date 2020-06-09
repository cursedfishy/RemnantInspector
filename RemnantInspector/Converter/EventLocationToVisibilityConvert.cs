using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemnantInspector.Converter
{
    public class EventLocationToVisibilityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return System.Windows.Visibility.Visible;
            }
            else
            {
                if(value.ToString() == App.campaignDisplayGroup)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
