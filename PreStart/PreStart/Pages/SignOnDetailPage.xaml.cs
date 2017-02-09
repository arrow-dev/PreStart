
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class SignOnDetailPage : ContentPage
    {
        public SignOnDetailPage()
        {
            InitializeComponent();
            
        }

        public SignOnDetailPage(SignOn signOn)
        {
            InitializeComponent();
            BindingContext = new SignOnDetailPageViewModel(signOn, Navigation);
        }
    }
}
