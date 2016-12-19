using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

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
