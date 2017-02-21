using Prestart.Abstractions;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class PrestartForm4ViewModel : PrestartBaseViewModel
    {
        public PrestartForm4ViewModel(Model.Prestart prestart, INavigation nav)
        {
            Navigation = nav;
            Prestart = prestart;
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

        Command nextCommand;
        public Command NextCommand
            => nextCommand ?? (nextCommand = new Command(async () => await ExecuteNextCommand()));

        async Task ExecuteNextCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var table = await App.CloudService.GetTableAsync<Model.Prestart>();
                await table.UpsertItemAsync(Prestart);
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
