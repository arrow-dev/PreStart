using Prestart.Abstractions;
using Prestart.Helpers;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class PrestartDetailViewModel : PrestartBaseViewModel
    {
        Model.Prestart prestart;
        public Model.Prestart Prestart
        {
            get
            {
                return prestart;
            }

            set { SetProperty(ref prestart, value, "Prestart"); }
        }

        public PrestartDetailViewModel(INavigation nav)
        {
            Navigation = nav;
            GetSelectedPrestart();
        }

        async void GetSelectedPrestart()
        {
            
            var table = await App.CloudService.GetTableAsync<Model.Prestart>();
            Prestart = await table.ReadItemAsync(Settings.SelectedPrestartId);
            
        }
    }
}
