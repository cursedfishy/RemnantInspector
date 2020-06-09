using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemnantInspector.Converter
{
    public class IsMissingToForegroundColorConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (bool.TryParse(value.ToString(), out bool raw))
            {
                return raw ? "#ffcc00" : "#ffffff";
            }
            else { return "#ffffff"; }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
