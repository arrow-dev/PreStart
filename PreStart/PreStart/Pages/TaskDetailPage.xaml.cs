using System;
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
            viewModel = new TaskDetailViewModel(task);
            BindingContext = viewModel;
        }

        private TaskDetailViewModel viewModel;

        private void TaskDetailPage_OnAppearing(object sender, EventArgs e)
        {
            viewModel.GetHazardsAsync();
        }
    }
}
