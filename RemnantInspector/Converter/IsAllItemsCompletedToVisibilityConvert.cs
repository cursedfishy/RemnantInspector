using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemnantInspector.Converter
{
    public class IsAllItemsCompletedToVisibilityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(bool.TryParse(value.ToString(), out bool bIsAllCompleted))
            {
                if (bIsAllCompleted)
                    return System.Windows.Visibility.Collapsed;
                else
                    return System.Windows.Visibility.Visible;
            }
            else
                return System.Windows.Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
