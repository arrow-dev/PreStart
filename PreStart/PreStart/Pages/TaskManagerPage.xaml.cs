using PreStart.ViewModels;
using System;
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

        private void AddBtn_OnClick(object sender, EventArgs e)
        {
            EditContainer.IsVisible = true;
        }
    }
}
