
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartForm2 : ContentPage
    {
        public PrestartForm2(Prestart prestart)
        {
            InitializeComponent();
            BindingContext = new PrestartForm2ViewModel(prestart);
        }
    }
}
