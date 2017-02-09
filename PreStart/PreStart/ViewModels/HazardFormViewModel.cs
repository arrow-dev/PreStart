using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using PreStart.Abstractions;
using PreStart.Models;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class HazardFormViewModel : BaseViewModel
    {
        public Hazard Hazard { get; set; }

        public HazardFormViewModel(INavigation navigation) : base(navigation)
        {
            Hazard = new Hazard();
        }

        private Command doneCommand;

        public Command DoneCommand
            => doneCommand ?? (doneCommand = new Command(async () => await ExecuteDoneCommand(),()=> false));

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
                if (Hazard.Id == null)
                {
                    await table.CreateItemAsync(Hazard);
                }
                else
                {
                    await table.UpdateItemAsync(Hazard);
                }
                

                //Sync with online table
                await App.CloudService.SyncOfflineCacheAsync();

            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult != null)
                {
                    foreach (var error in ex.PushResult.Errors)
                    {
                        var serverItem = error.Result.ToObject<Hazard>();
                        var localItem = error.Item.ToObject<Hazard>();

                        localItem.Version = serverItem.Version;
                        await error.UpdateOperationAsync(JObject.FromObject(localItem));
                    }
                }
            }
            finally
            {
                IsBusy = false;
                
                //Navigate to the task manager
                await Navigation.PopToRootAsync(true);
            }
        }







    }
}

