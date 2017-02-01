
using System;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class Menu : MasterDetailPage
    {

        public Menu()
        {
            InitializeComponent();
            Detail = new PrestartNavigationPage(new HomePage());
        }

        private void Site_Select_Button_OnClicked(object sender, EventArgs e)
        {
            Detail = new PrestartNavigationPage(new SiteListPage());
            IsPresented = false;
        }

        private void Prestart_Button_OnClicked(object sender, EventArgs e)
        {

            if (Helpers.Settings.DefaultSiteSetting != string.Empty)
            {
                Detail = new PrestartNavigationPage(new PrestartManagerPage(Helpers.Settings.DefaultSiteSetting));
                IsPresented = false;
            }
        }

        private void Task_Manager_Button_OnClicked(object sender, EventArgs e)
        {
            if (Helpers.Settings.DefaultSiteSetting != string.Empty)
            {
                Detail = new PrestartNavigationPage(new TaskManagerPage(Helpers.Settings.DefaultSiteSetting));
                IsPresented = false;
            }
        }

        private void Sign_In_Button_OnClicked(object sender, EventArgs e)
        {
            if (Helpers.Settings.DefaultSiteSetting != string.Empty)
            {
                Detail = new PrestartNavigationPage(new SignOnManager(Helpers.Settings.DefaultSiteSetting));
                IsPresented = false;
            }
        }
    }
}
