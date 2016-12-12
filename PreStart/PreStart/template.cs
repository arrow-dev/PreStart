using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PreStart
{
    static class template
    {
        public static readonly Thickness PagePadding =
            new Thickness(40, Device.OnPlatform(20, 20, 20), 40, 0);

        public static readonly Font TitleFont =
            Font.SystemFontOfSize(Device.OnPlatform(25, 20, 30), FontAttributes.Bold);

        public static readonly Color BackgroundColor =
            Device.OnPlatform(Color.White, Color.Black, Color.Black);

        public static readonly Color ForegroundColor =
            Device.OnPlatform(Color.Black, Color.White, Color.White);
    }
}
