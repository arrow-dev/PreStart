using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class PrestartForm3 : ContentPage
    {
        public PrestartForm3(Model.Prestart prestart)
        {
            InitializeComponent();
            BindingContext = new PrestartForm3ViewModel(prestart, Navigation);
        }
    }
}
