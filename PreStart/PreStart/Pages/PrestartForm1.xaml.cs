
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartForm1 : ContentPage
    {
        public PrestartForm1()
        {
            InitializeComponent();
            //Set the binding target to PrestartForm1 source
            BindingContext = new PrestartForm1ViewModel();


        }

        
    }


}
