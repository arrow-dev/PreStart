using PreStart.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Task = PreStart.Models.Task;

namespace PreStart.ViewModels
{
    public class TaskManagerViewModel : BaseViewModel
    {
        public string PrestartId;

        public TaskManagerViewModel(string prestartId, INavigation navigation) : base(navigation)
        {
            PrestartId = prestartId;
        }

        Task selectedItem;
        public Task SelectedItem
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
                    Navigation.PushAsync(new Pages.TaskDetailPage(selectedItem));
                    SelectedItem = null;
                }
            }
        }

        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set { SetProperty(ref taskName, value, "TaskName");}
        }

        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set { SetProperty(ref tasks, value, "Tasks");}
        }

        public async void GetTasksAsync()
        {
            var table = await App.CloudService.GetTableAsync<Task>();
            var items = await table.ReadAllItemsAsync();
            Tasks.Clear();
            foreach (var item in items)
            {
              
                    Tasks.Add(item);
                
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
                GetTasksAsync();
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
            => newCommand ?? (newCommand = new Command(async () => await ExecuteNewCommand(),  () => false));
        // Not working
        async System.Threading.Tasks.Task ExecuteNewCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                
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
                var table = await App.CloudService.GetTableAsync<Task>();
                var task = await table.ReadItemAsync(id);
                await table.DeleteItemAsync(task);
                GetTasksAsync();
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
