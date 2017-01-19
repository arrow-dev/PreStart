using PreStart.Abstractions;
using PreStart.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class HazardFormViewModel : BaseViewModel
    {
        public Hazard Hazard { get; set; }

        public HazardFormViewModel(Hazard hazard, INavigation navigation) : base(navigation)
        {
            Hazard = hazard;
        }
        

        private Command doneCommand;

        public Command DoneCommand
            => doneCommand ?? (doneCommand = new Command(async () => await ExecuteDoneCommand()));

        async Task ExecuteDoneCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {








                //Get Hazard sync table context
                var table = await App.CloudService.GetTableAsync<Hazard>();

                //Add Current Hazard to the table
                await table.CreateItemAsync(Hazard);

                //Sync with online table
                await App.CloudService.SyncOfflineCacheAsync();

                //Navigate to the task manager
                await Navigation.PopAsync(true);

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

