using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.Abstractions;
using PreStart.Models;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
  public  class PrestartDetailViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }


        public PrestartDetailViewModel(Prestart prestart, INavigation navigation) : base(navigation)
        {
            Prestart = prestart;
        }
    }
}
