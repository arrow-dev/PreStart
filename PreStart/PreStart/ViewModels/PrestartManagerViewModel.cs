using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PreStart.ViewModels
{
    class PrestartManagerViewModel : BaseViewModel
    {
        ObservableCollection<Prestart> preStarts = new ObservableCollection<Prestart>();

        public ObservableCollection<Prestart> Prestarts
        {
            get { return preStarts; }
            set { SetProperty(ref preStarts, value , "Prestarts");}
        }

        

        public PrestartManagerViewModel(INavigation navigation) : base(navigation)
        {
           
        }

        public async void GetPrestarts()
        {
            var table = await App.CloudService.GetTableAsync<Prestart>();
            var items = await table.ReadAllItemsAsync();
            Prestarts.Clear();
            foreach (var item in items)
            {
                
                    Prestarts.Add(item);
                
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
                GetPrestarts();
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
                
                //var tempMenu = new Menu();
                //tempMenu.ListView.SelectedItem = "Meeting Room";
                //await App.Current.MainPage.Navigation.PopToRootAsync();
                //await Navigation.PushAsync(tempMenu);
                var tempForm = new PrestartForm1();
                await Navigation.PushAsync(tempForm);


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
                var table = await App.CloudService.GetTableAsync<Prestart>();
                var prestart = await table.ReadItemAsync(id);
                await table.DeleteItemAsync(prestart);
                GetPrestarts();
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


        Prestart selectedItem;
        public Prestart SelectedItem
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
                    Helpers.Settings.SelectedPrestartId = SelectedItem.Id;
                    Navigation.PushAsync(new Pages.PrestartDetail(selectedItem));
                    SelectedItem = null;
                }
            }
        }

    }
}
