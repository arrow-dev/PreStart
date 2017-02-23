using Prestart.Abstractions;
using Prestart.View;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class PrestartForm2ViewModel : PrestartBaseViewModel
    {
        public PrestartForm2ViewModel(Model.Prestart prestart, INavigation nav)
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
                await Navigation.PushAsync(new PrestartForm3(Prestart));
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
