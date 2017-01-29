using PreStart.Abstractions;
using PreStart.Pages;
using PreStart.Services;
using Xamarin.Forms;

namespace PreStart
{
    public class App : Application
    {
        public static ICloudService CloudService { get; set; }

        public static SalesforceDataService SalesforceDataService { get; set; }

        public App()
        {
            CloudService = new AzureCloudService();
            SalesforceDataService = new SalesforceDataService();
            MainPage = new Test();
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
