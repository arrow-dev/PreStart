using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HazardDetailViewPage : ContentPage
    {
        public HazardDetailViewPage(Hazard hazard)
        {
            InitializeComponent();
            BindingContext = new HazardDetailViewModel(hazard);
        }

       
    }
}
