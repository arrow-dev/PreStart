using System;
using System.Collections.ObjectModel;
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class SiteListPage : ContentPage
    {
        private static SiteListPageViewModel SiteListVM = new SiteListPageViewModel();

        public SiteListPage()
        {
            InitializeComponent();
            BindingContext = new SiteListPageViewModel();
            
        }

        void OnSearchBarTextChanged(object sender, TextChangedEventArgs args)
        {
            resultsStack.Children.Clear();
        }

        void OnSearchBarButtonPressed(object sender, EventArgs args)
        {
            resultsScroll.Content = null;
            resultsStack.Children.Clear();
            SearchSite(SiteListVM.Sites);
            resultsScroll.Content = resultsStack;
        }

        void SearchSite(ObservableCollection<Location> Sites)
        {
            foreach (var item in Sites)
            {
                if (item.Name == SiteSearchBar.Text)
                {
                    
                }
            }
            
        }

    }
}
