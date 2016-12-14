using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartForm3 : ContentPage
    {
        public PrestartForm3(Prestart prestart)
        {
            InitializeComponent();
            BindingContext = new PrestartForm3ViewModel(prestart);
        }
    }
}
