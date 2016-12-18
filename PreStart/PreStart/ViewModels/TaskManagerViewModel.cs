using PreStart.Abstractions;
using System.Collections.ObjectModel;
using Task = PreStart.Models.Task;

namespace PreStart.ViewModels
{
    public class TaskManagerViewModel : BaseViewModel
    {
        public string PrestartId;
        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();

        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set { SetProperty(ref tasks, value, "Tasks");}
        }

        public TaskManagerViewModel(string prestartId)
        {
            PrestartId = prestartId;
            GetTasksAsync();
        }

        public async void GetTasksAsync()
        {
            var table = await App.CloudService.GetTableAsync<Task>();
            var items = await table.ReadAllItemsAsync();

            foreach (var item in items)
            {
                if (item.PrestartId == PrestartId)
                {
                    Tasks.Add(item);
                }
            }
        }
    }
}
