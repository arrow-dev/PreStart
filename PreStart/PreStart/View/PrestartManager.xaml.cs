using Prestart.Helpers;
using Prestart.ViewModel;
using System;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class PrestartManager : ContentPage
    {
        public PrestartManager()
        {
            InitializeComponent();
            BindingContext = new PrestartManagerViewModel(Navigation);
        }
    }
}
