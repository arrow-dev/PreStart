using System;
using System.Globalization;
using Xamarin.Forms;

namespace Prestart.ValueConverters
{
    class DateTimeToString : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime)
                return ((DateTime) value).ToString();

            return "No Date Found";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
