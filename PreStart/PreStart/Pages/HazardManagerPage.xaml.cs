using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HazardManagerPage : ContentPage
    {
        public HazardManagerPage()
        {
            InitializeComponent();
            BindingContext = new HazardManagerViewModel(Navigation);
        }
    }
}
