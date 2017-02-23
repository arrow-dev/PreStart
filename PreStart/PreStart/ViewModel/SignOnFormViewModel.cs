using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.Model;
using SignaturePad.Forms;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class SignOnFormViewModel : PrestartBaseViewModel
    {
        private SignaturePadView SignaturePad { get; set; }

        SignOn signOn;
        public SignOn SignOn
        {
            get { return signOn;}
            set { SetProperty(ref signOn, value, "SignOn"); }
        }

        public SignOnFormViewModel(INavigation nav, SignaturePadView sig)
        {
            Navigation = nav;
            SignaturePad = sig;
            SignOn = new SignOn {PrestartId = Settings.SelectedPrestartId};
        }

        Command agreeCommand;
        public Command AgreeCommand
            => agreeCommand ?? (agreeCommand = new Command(async () => await ExecuteAgreeCommand()));

        async Task ExecuteAgreeCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                SignOn.Signature = await GetSigBytesAsync();
                var table = await App.CloudService.GetTableAsync<SignOn>();
                await table.UpsertItemAsync(SignOn);
                await App.CloudService.SyncOfflineCacheAsync();
                await Application.Current.MainPage.DisplayAlert("Alert", "Data Saved", "OK");
                await Navigation.PopToRootAsync(true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task<byte[]> GetSigBytesAsync()
        {
            using (var memoryStream = new MemoryStream())
            {
                var stream = await SignaturePad.GetImageStreamAsync(SignatureImageFormat.Png);
                stream.Position = 0;
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}

