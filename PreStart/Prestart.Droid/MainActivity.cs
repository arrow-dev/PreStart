

using Android.App;
using Android.Content.PM;
using Android.OS;
using System;

namespace Prestart.Droid
{
    [Activity(Label = "Prestart", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true,
         ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        //TODO Remove when bug is fixed.
        protected override void OnDestroy()
        {
            try
            {
                base.OnDestroy();
            }
            catch (Exception)
            {
                
            }
        }
    }
}

