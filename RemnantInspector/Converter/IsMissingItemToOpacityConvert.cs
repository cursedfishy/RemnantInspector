using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RemnantInspector.Converter
{
    public class IsMissingItemToOpacityConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(bool.TryParse(value.ToString(), out bool isMissing))
            {
                if (isMissing)
                    return "1.0";
                else
                    return "0.5";
            }
            else
                return "0.5";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
