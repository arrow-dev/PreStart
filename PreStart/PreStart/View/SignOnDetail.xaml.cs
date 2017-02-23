using Prestart.Model;
using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class SignOnDetail : ContentPage
    {
        public SignOnDetail(SignOn signOn)
        {
            InitializeComponent();
            BindingContext = new SignOnDetailViewModel(Navigation, signOn);
        }
    }
}
