using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.Model;
using Prestart.View;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Prestart = Prestart.Model.Prestart;


//using Prestart = Prestart.Model.Prestart;

namespace Prestart.ViewModel
{
    class SignOnManagerViewModel : PrestartBaseViewModel
    {
        public SignOnManagerViewModel(INavigation nav)
        {
            Navigation = nav;
        }

        ObservableCollection<SignOn> items = new ObservableCollection<SignOn>();
        public ObservableCollection<SignOn> Items
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

        SignOn selectedItem;
        public SignOn SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value, "SelectedItem");
                if (selectedItem != null)
                {
                    Navigation.PushAsync(new SignOnDetail(selectedItem));
                    SelectedItem = null;
                }
            }
        }

        Model.Prestart _prestart;

        public Model.Prestart Prestart
        {
            get { return _prestart; }
            set { SetProperty(ref _prestart, value, "Prestart"); }
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
                Prestart =
                    await App.CloudService.GetTableAsync<Model.Prestart>()
                        .Result.ReadItemAsync(Settings.SelectedPrestartId);
                var table = await App.CloudService.GetTableAsync<SignOn>();
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
                if (Settings.SelectedPrestartId == "")
                {
                    await App.Current.MainPage.DisplayAlert("Select a Prestart", "You must select a Prestart From the Prestart Log to Sign On.", "Ok");
                    return;
                }
                await Navigation.PushAsync(new SignOnForm());
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
            MessagingCenter.Subscribe<SignOnManagerViewModel>(this, "ItemsChanged", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
        }
    }
}
