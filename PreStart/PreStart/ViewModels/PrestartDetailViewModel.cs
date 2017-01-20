using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.Abstractions;
using PreStart.Models;
using PropertyChanged;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
  public  class PrestartDetailViewModel : BaseViewModel
    {
        public Prestart Prestart { get; set; }

        private string createdAt;

        public string CreatedAt
        {
            get { return createdAt; }
            set { SetProperty(ref createdAt,value,"CreatedAt"); }
        }


        public PrestartDetailViewModel(Prestart prestart, INavigation navigation) : base(navigation)
        {
            Prestart = prestart;
            CreatedAt = prestart.CreatedAt.Value.LocalDateTime.ToString();
        }
    }
}
