using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm2ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }
        

        public PrestartForm2ViewModel(Prestart prestart, INavigation navigation) : base(navigation)
        {
            Prestart = prestart;
        }
        

        //Command that navigate to next page.
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
