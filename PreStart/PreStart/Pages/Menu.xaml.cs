
using System;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class Menu : MasterDetailPage
    {

        public Menu()
        {
            InitializeComponent();
        }

        private void Site_Select_Button_OnClicked(object sender, EventArgs e)
        {
            Detail = new SiteListPage();
            IsPresented = false;
        }

        private void Prestart_Button_OnClicked(object sender, EventArgs e)
        {

            if (Helpers.Settings.DefaultSiteSetting != string.Empty)
            {
                Detail = new PrestartManagerPage(Helpers.Settings.DefaultSiteSetting);
                IsPresented = false;
            }
        }

        private void Task_Manager_Button_OnClicked(object sender, EventArgs e)
        {
            if (Helpers.Settings.DefaultSiteSetting != string.Empty)
            {
                Detail = new TaskManagerPage(Helpers.Settings.DefaultSiteSetting);
                IsPresented = false;
            }
        }
    }
}
