using PreStart.ViewModels;
using Xamarin.Forms;
using Task = PreStart.Models.Task;

namespace PreStart.Pages
{
    public partial class TaskDetailPage : ContentPage
    {
        public TaskDetailPage(Task task)
        {
            InitializeComponent();
            BindingContext = new TaskDetailViewModel(task);
        }
    }
}
