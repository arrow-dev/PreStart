using Windows.Security.Cryptography.Core;
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartDetail : ContentPage
    {

        public PrestartDetail()
        {
            InitializeComponent();
            BindingContext = new PrestartDetailViewModel(Navigation);
        }

        public PrestartDetail(Prestart prestart)
        {
            InitializeComponent();

            BindingContext = new PrestartDetailViewModel(prestart, Navigation);
        }
    }
}
