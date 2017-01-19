using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartManagerPage : ContentPage
    {
        public PrestartManagerPage(string id)
        {
            InitializeComponent();
            BindingContext = new PrestartManagerViewModel(id, Navigation);
        }
    }
}
