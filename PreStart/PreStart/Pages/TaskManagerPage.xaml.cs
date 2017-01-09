using PreStart.ViewModels;
using System;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class TaskManagerPage : ContentPage
    {
        private TaskManagerViewModel ViewModel;
        public TaskManagerPage(string id)
        {
            InitializeComponent();
            ViewModel = new TaskManagerViewModel(id);
            BindingContext = ViewModel;
        }

        private void AddBtn_OnClick(object sender, EventArgs e)
        {
            if (AddBtn.Text == "+")
            {
                AddBtn.Text = "-";
                EditContainer.IsVisible = true;
            }
            else
            {
                AddBtn.Text = "+";
                EditContainer.IsVisible = false;
                Editor.Text = String.Empty;
            }
        }

        private void TaskManagerPage_OnAppearing(object sender, EventArgs e)
        {
            ViewModel.GetTasksAsync();
        }
    }
}

