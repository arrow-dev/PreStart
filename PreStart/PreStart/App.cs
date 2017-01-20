using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using PreStart.Services;
using Xamarin.Forms;

namespace PreStart
{
    public class App : Application
    {
        public static ICloudService CloudService { get; set; }

        static LocalData _database;
        public static LocalData Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new LocalData(DependencyService.Get<IFileHelper>().GetLocalFilePath("salesforcedata.db"));
                }
                return _database;
            }
        }

        public App()
        {
            CloudService = new AzureCloudService();
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
