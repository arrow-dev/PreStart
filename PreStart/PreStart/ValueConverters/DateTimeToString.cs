using System;
using System.Globalization;
using Xamarin.Forms;

namespace Prestart.ValueConverters
{
    class DateTimeToString : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTimeOffset)
                return ((DateTimeOffset)value).DateTime.ToString();

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
