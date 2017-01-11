using PreStart.Abstractions;
using PreStart.Models;
using System.Collections.ObjectModel;

namespace PreStart.ViewModels
{
    class SiteListPageViewModel : BaseViewModel
    {
        public SiteListPageViewModel()
        {
            GetSitesAsync();
        }

        private ObservableCollection<Site> _sites = new ObservableCollection<Site>();
        public ObservableCollection<Site> Sites
        {
            get { return _sites; }
            set { SetProperty(ref _sites, value, "Sites");}
        }

        public async void GetSitesAsync()
        {
            await App.CloudService.SyncOfflineCacheAsync();
            var table = await App.CloudService.GetTableAsync<Site>();
            var items = await table.ReadAllItemsAsync();
            Sites.Clear();
            foreach (var item in items)
            {
                Sites.Add(item);
            }
        }

        Site selectedItem;
        public Site SelectedItem
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
                    SelectedItem = null;
                }
            }
        }
    }
}
