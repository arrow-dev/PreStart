
using System;
using Prestart.Helpers;
using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class PrestartDetail : ContentPage
    {
        public PrestartDetail()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (Settings.SelectedPrestartId != string.Empty)
            {
                BindingContext = new PrestartDetailViewModel(Navigation);
                ErrorLayout.IsVisible = false;
                Details.IsVisible = true;
            }
            else
            {
                ErrorLayout.IsVisible = true;
                Details.IsVisible = false;
            }
        }

        private void PrestartLogBtn_Clicked(object sender, EventArgs e)
        {
            if (App.Current.MainPage is MasterDetailPage)
            {
                var masterDetail = (MasterDetailPage) App.Current.MainPage;
                masterDetail.Detail = new PrestartNavigationPage(new PrestartManager());
            }
        }
    }
}
