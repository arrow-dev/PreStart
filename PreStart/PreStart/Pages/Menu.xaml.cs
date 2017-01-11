
using System;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class Menu : MasterDetailPage
    {
        private bool SiteSelected;

        public Menu()
        {
            InitializeComponent();
            if (Helpers.Settings.DefaultSiteSetting != string.Empty)
            {
                SiteSelected = true;
            }
        }

        private void Site_Select_Button_OnClicked(object sender, EventArgs e)
        {
            Detail = new SiteListPage();
            IsPresented = false;
        }

        private void Prestart_Button_OnClicked(object sender, EventArgs e)
        {

            if (SiteSelected)
            {
                Detail = new PrestartManagerPage(Helpers.Settings.DefaultSiteSetting);
                IsPresented = false;
            }
        }

        private void Task_Manager_Button_OnClicked(object sender, EventArgs e)
        {
            if (SiteSelected)
            {
                Detail = new TaskManagerPage(Helpers.Settings.DefaultSiteSetting);
                IsPresented = false;
            }
        }
    }
}
