using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartManagerPage : ContentPage
    {
        public PrestartManagerPage()
        {
            InitializeComponent();
            BindingContext = new PrestartManagerViewModel(Navigation);
        }
    }
}
