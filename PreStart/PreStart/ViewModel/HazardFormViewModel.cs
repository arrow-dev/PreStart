using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.Model;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class HazardFormViewModel : PrestartBaseViewModel
    {
        Hazard hazard;
        public Hazard Hazard
        {
            get { return hazard; }
            set { SetProperty(ref hazard, value, "Hazard"); }
        }


        public HazardFormViewModel(INavigation nav)
        {
            Navigation = nav;
            Hazard = new Hazard {PrestartId = Settings.SelectedPrestartId};
        }

        Command doneCommand;
        public Command DoneCommand
            => doneCommand ?? (doneCommand = new Command(async () => await ExecuteDoneCommand()));

        async Task ExecuteDoneCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await App.CloudService.GetTableAsync<Hazard>().Result.CreateItemAsync(Hazard);
                await App.CloudService.SyncOfflineCacheAsync();
                await Application.Current.MainPage.DisplayAlert("Alert", "Data Saved", "OK");
                await Navigation.PopToRootAsync(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }







 


    }
}
