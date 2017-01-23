using PreStart.Abstractions;
using PreStart.Models;
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
            Sync();
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

        public async void Sync()
        {
            var response = await App.SalesforceDataService.GetOnlineData<Location>("select+id,name+from+location__c+where+country__c='New_Zealand'");
            var remote = response.records.ToList();
            var local = await App.SalesforceDataService.LocalData.Table<Location>().ToListAsync();
            
            foreach (var item in remote)
            {
                if (!local.Exists(i => i.Id == item.Id))
                {
                    await App.SalesforceDataService.LocalData.InsertAsync(item);
                }
            }
            GetLocalSitesAsync();
        }

        public async void GetLocalSitesAsync()
        {
            AllSites = await App.SalesforceDataService.LocalData.Table<Location>().OrderBy(l => l.Name).ToListAsync();
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
