using PreStart.ViewModels;
using System;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class PrestartManagerPage : ContentPage
    {
        private PrestartManagerViewModel Binding;
        private string Id;

        public PrestartManagerPage(string id)
        {
            InitializeComponent();
            Id = id;
            Binding = new PrestartManagerViewModel(Id);
            BindingContext = Binding;
        }

        private void PrestartManagerPage_OnAppearing(object sender, EventArgs e)
        {
            Binding.GetPrestarts(Id);
        }
    }
}
