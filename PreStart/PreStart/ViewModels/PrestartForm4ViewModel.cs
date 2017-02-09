using PreStart.Abstractions;
using PreStart.Models;
using System;
using System.Diagnostics;
using Windows.System.Display;
using Windows.UI.Notifications;
using PreStart.Pages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm4ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm4ViewModel(Prestart prestart, INavigation navigation) : base(navigation)
        {
            
            Prestart = prestart;
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
                //Get Prestart sync table context
                
                var table =  await App.CloudService.GetTableAsync<Prestart>();

                //Add Current Prestart to the table
                
                await table.CreateItemAsync(Prestart);

                //Sync with online table
                
                await App.CloudService.SyncOfflineCacheAsync();
     
                //Navigate to the task manager
                await Navigation.PopToRootAsync(true);


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
