using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.View;
using System;
using System.Diagnostics;
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

        Command editCommand;
        public Command EditCommand
            => editCommand ?? (editCommand = new Command(async () => await ExecuteEditCommand()));

        async System.Threading.Tasks.Task ExecuteEditCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await Navigation.PushAsync(new PrestartForm1(Prestart));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
