using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class HazardManagerViewModel : BaseViewModel
    {
        public HazardManagerViewModel(INavigation navigation) : base(navigation)
        {

        }

        private ObservableCollection<Hazard> hazards = new ObservableCollection<Hazard>();
        public ObservableCollection<Hazard> Hazards
        {
            get { return hazards; }
            set { SetProperty(ref hazards, value, "Hazards"); }
        }

        public async void GetHazardsAsync()
        {
            var table = await App.CloudService.GetTableAsync<Hazard>();
            var items = await table.ReadAllItemsAsync();
            Hazards.Clear();
            foreach (var item in items)
            {
                Hazards.Add(item);
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
                GetHazardsAsync();
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

        Hazard selectedItem;
        public Hazard SelectedItem
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
                    Navigation.PushAsync(new Pages.HazardDetailViewPage(selectedItem));
                    SelectedItem = null;
                }
            }
        }

        private Command addHazard;

        public Command AddHazard
            => addHazard ?? (addHazard = new Command(async () => await ExecuteAddHazard()));

        async System.Threading.Tasks.Task ExecuteAddHazard()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await Navigation.PushAsync(new HazardForm());
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


        private Command deleteCommand;

        public Command DeleteCommand
            =>
            deleteCommand ?? (deleteCommand = new Command<string>(async (string id) => await ExecuteDeleteHazard(id)));

        async System.Threading.Tasks.Task ExecuteDeleteHazard(String id)
        {
            if(IsBusy)
                return;
            IsBusy = true;

            try
            {
                var hazard_table = await App.CloudService.GetTableAsync<Hazard>();
                var aHazard = await hazard_table.ReadItemAsync(id);
                await hazard_table.DeleteItemAsync(aHazard);
                GetHazardsAsync();
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
    }
}
