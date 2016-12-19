using System.Collections.ObjectModel;
using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {

        public Task Task { get; set; }
        public ObservableCollection<Hazard> Hazards { get; set; }


        public TaskDetailViewModel(Task task)
        {
            Task = task;
        }

        


    }
}
