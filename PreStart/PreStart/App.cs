using PreStart.Abstractions;
using PreStart.Pages;
using PreStart.Services;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart
{
    public class App : Application
    {
        public static ICloudService CloudService { get; set; }

        public App()
        {
            CloudService = new AzureCloudService();
            MainPage = new HazardDetailViewPage();
            
            //MainPage = new NavigationPage(new DemoPage());
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
