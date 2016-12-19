using PreStart.Pages;
using PreStart.Services;
using Xamarin.Forms;

namespace PreStart
{
    public class App : Application
    {
        public static AzureCloudService CloudService { get; set; }

        public App()
        {
            CloudService = new AzureCloudService();
            MainPage = new NavigationPage(new PrestartForm1());
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
