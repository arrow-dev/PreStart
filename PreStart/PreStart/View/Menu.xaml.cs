using System;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class Menu : MasterDetailPage
    {
        public Menu()
        {
            InitializeComponent();
            Detail = new PrestartNavigationPage(new PrestartManager());
        }

        private void SignOnButton_OnClicked(object sender, EventArgs e)
        {
            Detail = new PrestartNavigationPage(new Main());
            IsPresented = false;
        }

        private void PrestartLogButton_OnClicked(object sender, EventArgs e)
        {
            Detail = new PrestartNavigationPage(new PrestartManager());
            IsPresented = false;
        }
    }
}
