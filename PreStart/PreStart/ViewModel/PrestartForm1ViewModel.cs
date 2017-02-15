using Prestart.Abstractions;
using Prestart.View;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class PrestartForm1ViewModel : PrestartBaseViewModel
    {
        public PrestartForm1ViewModel(INavigation nav)
        {
            Prestart = new Model.Prestart();
            Navigation = nav;
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
                await Navigation.PushAsync(new PrestartForm2(Prestart));
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
