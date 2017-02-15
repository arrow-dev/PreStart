using Prestart.Abstractions;
using Prestart.Helpers;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class PrestartDetailViewModel : PrestartBaseViewModel
    {
        public PrestartDetailViewModel(INavigation nav)
        {
            Navigation = nav;
            GetSelectedPrestart();
        }

        Model.Prestart prestart;
        public Model.Prestart Prestart
        {
            get
            {
                return prestart;
            }

            set { SetProperty(ref prestart, value, "Prestart"); }
        }

        async void GetSelectedPrestart()
        {
            
            var table = App.CloudService.GetTable<Model.Prestart>();
            Prestart = await table.ReadItemAsync(Settings.SelectedPrestartId);
            
        }
    }
}
