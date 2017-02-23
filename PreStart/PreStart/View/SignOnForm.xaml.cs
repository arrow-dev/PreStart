
using Prestart.ViewModel;
using System;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class SignOnForm : ContentPage
    {
        public SignOnForm()
        {
            InitializeComponent();
            BindingContext = new SignOnFormViewModel(Navigation, padView);
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Form.IsVisible = false;
            SigPad.IsVisible = true;
        }
    }
}
