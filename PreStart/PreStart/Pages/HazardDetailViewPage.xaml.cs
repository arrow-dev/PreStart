using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HazardDetailViewPage : ContentPage
    {
        public HazardDetailViewPage()
        {
            InitializeComponent();

       

            BindingContext = new HazardDetailViewModel();
        }

       
    }
}
