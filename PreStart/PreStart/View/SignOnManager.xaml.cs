using Prestart.ViewModel;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class SignOnManager : ContentPage
    {
        public SignOnManager()
        {
            InitializeComponent();
            BindingContext = new SignOnManagerViewModel(Navigation);
        }
    }
}
