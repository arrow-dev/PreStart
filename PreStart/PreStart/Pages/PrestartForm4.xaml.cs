using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartForm4 : ContentPage
	{
		public PrestartForm4 (Prestart prestart)
		{
			InitializeComponent ();
            BindingContext = new PrestartForm4ViewModel(prestart);
		}
	}
}
