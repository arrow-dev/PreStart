using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        public Task Task;

        public TaskDetailViewModel(Task task)
        {
            Task = task;
        }
    }
}
