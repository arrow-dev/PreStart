using MvvmHelpers;
using Xamarin.Forms;

namespace Prestart.Abstractions
{
    class PrestartBaseViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
    }
}
