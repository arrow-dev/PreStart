using PreStart.Abstractions;
using PreStart.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PreStart.Pages;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class SignOnManagerViewModel : BaseViewModel
    {
        public SignOnManagerViewModel(string id, INavigation navigation) : base(navigation)
        {
            Id = id;
        }

        ObservableCollection<SignOn> signOns = new ObservableCollection<SignOn>();

        public ObservableCollection<SignOn> SignOns
        {
            get { return signOns; }
            set { SetProperty(ref signOns, value, "SignOns"); }
        }

        private string Id;

        public async void GetSignOns(string id)
        {
            var table = await App.CloudService.GetTableAsync<SignOn>();
            var items = await table.ReadAllItemsAsync();
            SignOns.Clear();
            foreach (var item in items)
            {
                if (item.SiteId == id)
                {
                    SignOns.Add(item);
                }
            }
        }
        Command refreshCommand;
        public Command RefreshCommand
            => refreshCommand ?? (refreshCommand = new Command(async () => await ExecuteRefreshCommand()));

        async System.Threading.Tasks.Task ExecuteRefreshCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                GetSignOns(Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command newCommand;
        public Command NewCommand
            => newCommand ?? (newCommand = new Command(async () => await ExecuteNewCommand()));

        async System.Threading.Tasks.Task ExecuteNewCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                await Navigation.PushAsync(new SignaturePage(new SignOn{SiteId = Id}));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command deleteCommand;

        public Command DeleteCommand
            => deleteCommand ?? (deleteCommand = new Command<string>(async (string id) => await ExecuteDeleteCommand(id)));

        async System.Threading.Tasks.Task ExecuteDeleteCommand(String id)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var table = await App.CloudService.GetTableAsync<SignOn>();
                var signOn = await table.ReadItemAsync(id);
                await table.DeleteItemAsync(signOn);
                GetSignOns(Id);
                await App.CloudService.SyncOfflineCacheAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }


        SignOn selectedItem;
        public SignOn SelectedItem
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
                    //When an item is selected from the list then navigate to the details page passing the selected item through.
                    Navigation.PushAsync(new SignOnDetailPage(selectedItem));
                    
                    SelectedItem = null;
                }
            }
        }
    }
}