using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        async Task ExecuteLoginCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await App.CloudService.LoginAsync();
                var identity = await App.CloudService.GetIdentityAsync();
                var name = identity.UserClaims.FirstOrDefault(c => c.Type.Equals("name")).Value;
                await DisplayAlert("Welcome", "Welcome to Prestart " + name, "OK");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ExecuteLoginCommand] Error = {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PrestartForm1());
        }
    }
}
