using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class PrestartForm1ViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        public PrestartForm1ViewModel()
        {
            Prestart = new Prestart();
        }
    }
}
