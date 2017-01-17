using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm3ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm3ViewModel(Prestart prestart)
        {
            Prestart = prestart;
        }

        Command nextCommand;

        public Command NextCommand
            => nextCommand ?? (nextCommand = new Command(async () => await ExecuteNextCommand(), () => false));

        async Task ExecuteNextCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new PrestartForm4(Prestart));
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
