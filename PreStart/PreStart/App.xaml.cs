using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.Services;
using Prestart.View;
using Xamarin.Forms;

namespace Prestart
{
    public partial class App : Application
    {
        public static ICloudService CloudService { get; set; }

        public App()
        {
            InitializeComponent();
            CloudService = new AzureCloudService();
            Settings.SelectedPrestartId = string.Empty;
            MainPage = new Menu();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
