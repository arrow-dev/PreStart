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
    public partial class SignaturePage : ContentPage
    {
        

        public SignaturePage(SignOn signOn)
        {
            InitializeComponent();
            BindingContext = new SignOnFormViewModel(signOn, Navigation);
        }
    }
}
