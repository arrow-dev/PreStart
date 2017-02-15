using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class PrestartForm1 : ContentPage
    {
        public PrestartForm1()
        {
            InitializeComponent();
            BindingContext = new PrestartForm1ViewModel(Navigation);
        }
    }
}
