using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.Models;

namespace PreStart.ViewModels
{
  public  class PrestartDetailViewModel
    {
        public Prestart Prestart { get; set; }


        public PrestartDetailViewModel(Prestart prestart)
        {
            Prestart = prestart;
        }
    }
}
