using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class PrestartForm2ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm2ViewModel(Prestart prestart)
        {
            Prestart = prestart;
        }
    }
}
