using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class HazardManager : ContentPage
    {
        public HazardManager()
        {
            InitializeComponent();
            BindingContext = new HazardManagerViewModel(Navigation);
        }
    }
}
