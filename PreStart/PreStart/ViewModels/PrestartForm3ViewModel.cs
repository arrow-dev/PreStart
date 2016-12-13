using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class PrestartForm3ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm3ViewModel(Prestart prestart)
        {
            Prestart = prestart;
        }
    }
}
