using Prestart.Abstractions;
using Prestart.Model;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class SignOnDetailViewModel : PrestartBaseViewModel
    {
        SignOn signOn;
        public SignOn SignOn
        {
            get { return signOn; }
            set { SetProperty(ref signOn, value, "SignOn"); }
        }

        public SignOnDetailViewModel(INavigation nav, SignOn signOn)
        {
            Navigation = nav;
            SignOn = signOn;
        }
    }
}
