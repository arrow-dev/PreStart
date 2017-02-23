using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class PrestartForm2 : ContentPage
    {
        public PrestartForm2(Model.Prestart prestart)
        {
            InitializeComponent();
            BindingContext = new PrestartForm2ViewModel(prestart, Navigation);
        }
    }
}
