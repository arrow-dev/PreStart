using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm4ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm4ViewModel(Prestart prestart)
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
                var data = await table.ReadAllItemsAsync();

                //Sync with online table
                await App.CloudService.SyncOfflineCacheAsync();
                
                //Navigate to the task manager
                Application.Current.MainPage = new TaskManagerPage();
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
