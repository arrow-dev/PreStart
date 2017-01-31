using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using PreStart.Abstractions;
using PreStart.Models;
using SignaturePad.Forms;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class SignOnFormViewModel : BaseViewModel
    {
        public SignOn SignOn { get; set; }

        private SignaturePadView SignaturePad { get; set; }

        public SignOnFormViewModel(SignOn signOn, INavigation navigation, SignaturePadView signaturePadView) : base(navigation)
        {
            SignOn = signOn;
            SignaturePad = signaturePadView;
        }

        public async Task<byte[]> GetSigBytes()
        {
            using (var memoryStream = new MemoryStream())
            {
                var stream = await SignaturePad.GetImageStreamAsync(SignatureImageFormat.Png);
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        Command agreeCommand;
        public Command AgreeCommand
            => agreeCommand ?? (agreeCommand = new Command(async () => await ExecuteRefreshCommand()));

        async System.Threading.Tasks.Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                SignOn.Signature = await GetSigBytes();
                //update the online table
                //Get the online table
                var signon_form_table = await App.CloudService.GetTableAsync<SignOn>();
                //Add the current item to the table
                if (SignOn.Id == null)
                {
                    await signon_form_table.CreateItemAsync(SignOn);

                }
                else
                {
                    await signon_form_table.UpdateItemAsync(SignOn);
                }

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
                //Pop the current page and navigate back to the sign on record page
                await Navigation.PopAsync(true);
            }
        }
    }
}
