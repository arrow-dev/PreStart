using Prestart.Model;
using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class HazardDetail : ContentPage
    {
        public HazardDetail(Hazard haz)
        {
            InitializeComponent();
            BindingContext = new HazardDetailViewModel(Navigation, haz);
        }
    }
}
