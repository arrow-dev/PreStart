using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class SignaturePage : ContentPage
    {

        public SignaturePage()
        {
            InitializeComponent();
            BindingContext = new SignOnFormViewModel(Navigation, padView);
        }
        public SignaturePage(SignOn signOn)
        {
            InitializeComponent();
            BindingContext = new SignOnFormViewModel(signOn, Navigation, padView);
        }
    }
}
