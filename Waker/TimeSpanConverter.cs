using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Waker
{
    class TimeSpanToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var span = (TimeSpan)value;
                if (span < TimeSpan.FromSeconds(1))
                    return @"00:00";
                else if (span < TimeSpan.FromMinutes(1))
                    return span.ToString("%s")+"s";
                else if (span < TimeSpan.FromHours(1))
                    return span.ToString(@"m\:ss");
                else if (span < TimeSpan.FromDays(1))
                    return span.ToString(@"h\.mm\:ss");
                else
                    return span.ToString(@"d\ h\.mm");
            }
            catch
            {

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Parse(value as string);
        }
    }
    class TimeSpanToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var span = (TimeSpan)value;
                if (span < TimeSpan.FromMinutes(1))
                    return new SolidBrush(Color.Red);
                else if (span < TimeSpan.FromMinutes(3))
                    return new SolidBrush(Color.Blue);
                else
                    return new SolidBrush(Color.Black);
            }
            catch
            {

            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.Parse(value as string);
        }
    }
}
