using PreStart.Abstractions;
using PreStart.Pages;
using PreStart.Services;
using Xamarin.Forms;

namespace PreStart
{
    public class App : Application
    {
        public static ICloudService CloudService { get; set; }

        public App()
        {
            CloudService = new AzureCloudService();
            MainPage = new NavigationPage(new TaskManagerPage("1e019318-1b37-4dad-96e6-16355a8b85ca"));
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
