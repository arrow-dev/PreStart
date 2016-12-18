using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class TaskManagerPage : ContentPage
    {
        public TaskManagerPage(string id)
        {
            InitializeComponent();
            BindingContext = new TaskManagerViewModel(id);
        }
    }
}
