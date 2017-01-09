using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        private Task task;
        public Task Task {
            get { return task; }
            set { SetProperty(ref task, value, "Task");} }

        public TaskDetailViewModel(Task task)
        {
            Task = task;
        }

        private ObservableCollection<Hazard> hazards = new ObservableCollection<Hazard>();
        public ObservableCollection<Hazard> Hazards
        {
            get { return hazards; }
            set { SetProperty(ref hazards, value, "Tasks"); }
        }

        public async void GetHazardsAsync()
        {
            var table = await App.CloudService.GetTableAsync<Hazard>();
            var items = await table.ReadAllItemsAsync();
            Hazards.Clear();
            foreach (var item in items)
            {
                if (item.TaskId == Task.Id)
                {
                    Hazards.Add(item);
                }
            }
        }

        Hazard selectedItem;
        public Hazard SelectedItem
        {
            get { return selectedItem; }
            set
            {
                SetProperty(ref selectedItem, value, "SelectedItem");
                if (selectedItem != null)
                {
                    //When an item is selected from the list then navigate to the details page passing the selected item through.
                    Application.Current.MainPage.Navigation.PushAsync(new Pages.HazardDetailViewPage(selectedItem));
                    SelectedItem = null;
                }
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
                Application.Current.MainPage.Navigation.PushAsync(new HazardForm(new Hazard {TaskId = Task.Id}));
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
                var table = await App.CloudService.GetTableAsync<Hazard>();
                var hazard = await table.ReadItemAsync(id);
                await table.DeleteItemAsync(hazard);
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

