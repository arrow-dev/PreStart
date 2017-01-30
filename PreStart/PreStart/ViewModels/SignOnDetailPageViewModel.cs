using PreStart.Abstractions;
using PreStart.Models;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    class SignOnDetailPageViewModel : BaseViewModel
    {
        public SignOn SignOn { get; set; }

        public SignOnDetailPageViewModel(SignOn signOn, INavigation navigation) : base(navigation)
        {
            SignOn = signOn;
        }
    }
}
