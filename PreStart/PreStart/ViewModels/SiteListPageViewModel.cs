using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Services;
using System.Collections.ObjectModel;

namespace PreStart.ViewModels
{
    class SiteListPageViewModel : BaseViewModel
    {
        public SiteListPageViewModel()
        {
            GetSitesAsync();
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
            var response = await salesForce.GetData<Location>("select+id,name+from+location__c");
            var items = response.records;
            //await App.CloudService.SyncOfflineCacheAsync();
            //var table = await App.CloudService.GetTableAsync<Site>();
            //var items = await table.ReadAllItemsAsync();
            Sites.Clear();
            foreach (var item in items)
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
