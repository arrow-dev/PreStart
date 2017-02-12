
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartForm1 : ContentPage
    {

        public PrestartForm1()
        {
            InitializeComponent();
            BindingContext = new PrestartForm1ViewModel(new Prestart() , Navigation);
        }

        
    }


}
