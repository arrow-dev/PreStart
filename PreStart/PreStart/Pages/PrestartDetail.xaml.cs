using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartDetail : ContentPage
    {
        public PrestartDetail(Prestart prestart)
        {
            InitializeComponent();

            BindingContext = new PrestartDetailViewModel(prestart);
        }
    }
}
