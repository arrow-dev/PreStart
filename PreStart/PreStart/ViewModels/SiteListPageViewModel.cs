using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    class SiteListPageViewModel : BaseViewModel
    {
        private IEnumerable<Location> AllSites;

        public SiteListPageViewModel(INavigation navigation) : base(navigation)
        {
            GetSitesAsync();
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                SetProperty(ref searchText, value, "SearchText");
                filter(SearchText);
            }
        }

        private void filter(string query)
        {
            var filteredSites = AllSites.Where(s => s.Name.ToLower().Contains(query.ToLower())).ToList();
            Sites.Clear();
            foreach (var item in filteredSites)
            {
                Sites.Add(item);
            }
        }

        private ObservableCollection<Location> _sites = new ObservableCollection<Location>();
        public ObservableCollection<Location> Sites
        {
            get { return _sites; }
            set { SetProperty(ref _sites, value, "Sites");}
        }

        public async void GetSitesAsync()
        {
            var salesForce = new SalesforceDataService();
            var response = await salesForce.GetData<Location>("select+id,name+from+location__c+where+country__c='New_Zealand'");
            AllSites = response.records.OrderBy(i => i.Name);
            //await App.CloudService.SyncOfflineCacheAsync();
            //var table = await App.CloudService.GetTableAsync<Site>();
            //var items = await table.ReadAllItemsAsync();
            Sites.Clear();
            foreach (var item in AllSites)
            {
                Sites.Add(item);
                if (item.Id == Helpers.Settings.DefaultSiteSetting)
                {
                    SelectedItem = item;
                }
            }
        }

        Location selectedItem;
        public Location SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (IsBusy)
                {
                    return;
                }
                SetProperty(ref selectedItem, value, "SelectedItem");
                if (selectedItem != null)
                {
                    
                    Helpers.Settings.DefaultSiteSetting = selectedItem.Id;
                }
            }
        }
    }
}
