using System;
using System.Diagnostics;
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
            ExecuteLoginCommand();
        }
    }
}
