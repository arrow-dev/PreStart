using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class PrestartForm4ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm4ViewModel(Prestart prestart)
        {
            Prestart = prestart;
        }
    }
}
