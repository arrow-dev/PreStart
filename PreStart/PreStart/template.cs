using Xamarin.Forms;

namespace Prestart
{
    static class Template
    {
        public static readonly Thickness PagePadding =
            new Thickness(40, Device.OnPlatform(20, 20, 20), 40, 0);

        public static readonly Font TitleFont =
            Font.SystemFontOfSize(Device.OnPlatform(25, 20, 30), FontAttributes.Bold);

        public static readonly Font ParaFont =
            Font.SystemFontOfSize(Device.OnPlatform(10, 10, 10), FontAttributes.Italic);

        public static readonly Color BackgroundColor =
            Device.OnPlatform(Color.White, Color.Black, Color.Black);

        public static readonly Color ForegroundColor =
            Device.OnPlatform(Color.Black, Color.White, Color.White);

        public static readonly Thickness StackLayoutSection =
            new Thickness(40, 0, 40, 20);


        public static readonly Thickness StackLayoutTitle =
            new Thickness(40, 0, 20, 10);


        public static readonly Color fhBlue = Color.FromHex("#0099C7");

        public static readonly Color fhOrange = Color.FromHex("#FF671B");

        public static readonly Color fhGrey = Color.FromHex("#666");

        public static readonly Thickness ButtonPadding = new Thickness(10, 5, 10, 5);
    }
}
