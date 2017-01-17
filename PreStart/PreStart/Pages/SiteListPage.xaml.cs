using System;
using System.Collections.ObjectModel;
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class SiteListPage : ContentPage
    {
       
        public SiteListPage()
        {
            InitializeComponent();
            BindingContext = new SiteListPageViewModel();
            
        }
        
    }
}
