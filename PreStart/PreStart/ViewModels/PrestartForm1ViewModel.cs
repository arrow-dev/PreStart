using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm1ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm1ViewModel()
        {
            Prestart = new Prestart();
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
                Application.Current.MainPage = new PrestartForm2(Prestart);
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
