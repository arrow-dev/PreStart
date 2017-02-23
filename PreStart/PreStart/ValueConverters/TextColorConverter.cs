using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ValueConverters
{
    class TextColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color textColor = Color.Transparent;
            if (value != null)
            {
                string label = value.ToString().ToUpper();
                switch (label)
                {
                    case "LOW":
                        textColor = Color.White;
                        break;
                    case "MEDIUM":
                        textColor = Color.Gray;
                        break;
                    case "HIGH":
                        textColor = Color.White;
                        break;
                    case "EXTREME":
                        textColor = Color.White;
                        break;
                }
            }


            return textColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
