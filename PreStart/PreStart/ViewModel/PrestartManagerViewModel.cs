using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.View;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class PrestartManagerViewModel : PrestartBaseViewModel
    {
        public PrestartManagerViewModel(INavigation nav)
        {
            Navigation = nav;
        }

        int selectedFilter;
        public int SelectedFilter
        {
            get { return selectedFilter;}
            set
            {
                SetProperty(ref selectedFilter, value, "SelectedFilter");
                FilterHandler();
            }
        }

        ObservableCollection<Model.Prestart> items = new ObservableCollection<Model.Prestart>();
        public ObservableCollection<Model.Prestart> Items
        {
            get { return items; }
            set { SetProperty(ref items, value, "Items"); }
        }

        ObservableCollection<Model.Prestart> filteredPrestarts = new ObservableCollection<Model.Prestart>();
        public ObservableCollection<Model.Prestart> FilteredPrestarts
        {
            get { return filteredPrestarts; }
            set { SetProperty(ref filteredPrestarts, value, "FilteredPrestarts"); }
        }

        private void FilterHandler()
        {
            switch (SelectedFilter)
            {
                case 0:
                    Filter(Search, prestart => prestart.SiteManager);
                    break;
                case 1:
                    Filter(Search, prestart => prestart.JobNumber);
                    break;
                case 2:
                    Filter(Search, prestart => prestart.ContractName);
                    break;
            }
        }

        string search = String.Empty;
        public string Search
        {
            get { return search; }
            set
            {
                SetProperty(ref search, value, "Search");
                FilterHandler();
            }
        }

        private void Filter(string query, Func<Model.Prestart, string> prop )
        {
            var filteredList = Items.Where(i => prop(i).ToLower().Contains(query.ToLower())).ToList();
            FilteredPrestarts.Clear();
            foreach (var item in filteredList)
            {
                FilteredPrestarts.Add(item);
            }
        }

        bool showError;
        public bool ShowError
        {
            get { return showError; }
            set { SetProperty(ref showError, value, "ShowError"); }
        }

        Model.Prestart selectedItem;
        public Model.Prestart SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value, "SelectedItem");
                if (selectedItem != null)
                {
                    Settings.SelectedPrestartId = SelectedItem.Id;
                    if (App.Current.MainPage is MasterDetailPage)
                    {
                        var page = (MasterDetailPage) App.Current.MainPage;
                        page.Detail = new PrestartNavigationPage(new Main());
                    }
                    SelectedItem = null;
                }
            }
        }

        Command refreshCmd;
        public Command RefreshCommand => refreshCmd ?? (refreshCmd = new Command(async () => await ExecuteRefreshCommand()));

        async Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await App.CloudService.SyncOfflineCacheAsync();
                var table = await App.CloudService.GetTableAsync<Model.Prestart>();
                var list = await table.ReadItemsAfterDateAsync(DateTime.Now.StartOfWeek(DayOfWeek.Monday));
                ShowError = list.Count == 0;
                Items.Clear();
                foreach (var item in list)
                    Items.Add(item);
                FilteredPrestarts.Clear();
                foreach (var item in Items)
                    FilteredPrestarts.Add(item);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error loading items: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command addNewCmd;
        public Command AddNewItemCommand => addNewCmd ?? (addNewCmd = new Command(async () => await ExecuteAddNewItemCommand()));

        async Task ExecuteAddNewItemCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await Navigation.PushAsync(new PrestartForm1());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[TaskList] Error in AddNewItem: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task RefreshList()
        {
            await ExecuteRefreshCommand();
            MessagingCenter.Subscribe<PrestartManagerViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
        }
    }
}
