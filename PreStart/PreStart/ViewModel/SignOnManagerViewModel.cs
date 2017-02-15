using Prestart.Abstractions;
using Prestart.Helpers;
using Prestart.Model;
using Prestart.View;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        SignOn selectedItem;
        public SignOn SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value, "SelectedItem");
                if (selectedItem != null)
                {
                    //Navigate to SignOnDetail
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
                var table = App.CloudService.GetTable<SignOn>();
                var list = await table.ReadAllItemsAsync();
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
