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
            BindingContext = new TaskManagerViewModel(id, Navigation);
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
    }
}

