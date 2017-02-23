using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class PrestartForm4 : ContentPage
    {
        public PrestartForm4(Model.Prestart prestart)
        {
            InitializeComponent();
            BindingContext = new PrestartForm4ViewModel(prestart, Navigation);
        }
    }
}
