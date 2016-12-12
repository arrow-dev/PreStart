using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PreStart.Pages;
using Xamarin.Forms;

namespace PreStart
{
    public class App : Application
    {
        public App()
        {
           
            MainPage = new PrestartForm2();
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
