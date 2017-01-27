using Color = Xamarin.Forms.Color;
using NativeColor = Windows.UI.Color;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;


namespace SignaturePad.Forms.Platform
{
    public static class ColorExtensions
    {

        public static NativeColor ToWindows(this Color color)
        {
            return NativeColor.FromArgb(
                (byte)(color.A * 255),
                (byte)(color.R * 255),
                (byte)(color.G * 255),
                (byte)(color.B * 255));
        }


        public static NativeColor ToNative(this Color color)
        {

            return color.ToWindows();

        }


        public static void SetTextColor(this TextBlock textBlock, Color color)
        {
            textBlock.Foreground = new SolidColorBrush(color.ToNative());
        }

    }
}
