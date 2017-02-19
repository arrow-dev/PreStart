using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.View;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

        ObservableCollection<Model.Prestart> items = new ObservableCollection<Model.Prestart>();
        public ObservableCollection<Model.Prestart> Items
        {
            get { return items; }
            set { SetProperty(ref items, value, "Items"); }
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
                var table = await App.CloudService.GetTableAsync<Model.Prestart>();
                var list = await table.ReadItemsAfterDateAsync(DateTime.Now.StartOfWeek(DayOfWeek.Monday));
                ShowError = list.Count == 0;
                Items.Clear();
                foreach (var item in list)
                    Items.Add(item);
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
