using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class SignOnManager : ContentPage
    {
        public SignOnManager(string id)
        {
            InitializeComponent();
            BindingContext = new SignOnManagerViewModel(id, Navigation);
        }
    }
}
