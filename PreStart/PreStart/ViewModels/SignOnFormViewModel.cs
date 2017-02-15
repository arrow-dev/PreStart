using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using PreStart.Abstractions;
using PreStart.Models;
using SignaturePad.Forms;
using System.IO;
using System.Threading.Tasks;
using PreStart.Pages;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class SignOnFormViewModel : BaseViewModel
    {
        public SignOn SignOn { get; set; }

        private SignaturePadView SignaturePad { get; set; }

        public SignOnFormViewModel(INavigation navigation, SignaturePadView signaturePadView) : base(navigation)
        {
            SignOn = new SignOn();
            SignaturePad = signaturePadView;
        }
        //public SignOnFormViewModel(SignOn signOn, INavigation navigation, SignaturePadView signaturePadView) : base(navigation)
        //{
        //    SignOn = signOn;
        //    SignaturePad = signaturePadView;
        //}

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
            => agreeCommand ?? (agreeCommand = new Command(async () => await ExecuteCommand()));

        async System.Threading.Tasks.Task ExecuteCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                SignOn.Signature = await GetSigBytes();
                //update the online table
                //Get the online table
                //var table = await App.CloudService.GetTableAsync<SignOn>();
                //Add the current item to the table
                //if (SignOn.Id == null)
                //{
                //    await table.CreateItemAsync(SignOn);
                //}
                //else
                //{
                //    await table.UpdateItemAsync(SignOn);
                //}
                //await App.CloudService.SyncOfflineCacheAsync();

                var table = await App.CloudService.GetTableAsync<SignOn>();
                
                await table.CreateItemAsync(SignOn);
                
                await App.CloudService.SyncOfflineCacheAsync();
                
                await Navigation.PushAsync(new SignOnManager());

            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult != null)
                {
                    foreach (var error in ex.PushResult.Errors)
                    {
                        var serverItem = error.Result.ToObject<SignOn>();
                        var localItem = error.Item.ToObject<SignOn>();

                        localItem.Version = serverItem.Version;
                        await error.UpdateOperationAsync(JObject.FromObject(localItem));
                    }
                }
            }
            finally
            {
                IsBusy = false;
               // await Navigation.PushAsync(new HomePage());

            }
        }
    }
}
