﻿using PreStart.Abstractions;
using PreStart.Pages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Task = PreStart.Models.Task;

namespace PreStart.ViewModels
{
    public class TaskManagerViewModel : BaseViewModel
    {
        public string PrestartId;

        public TaskManagerViewModel(string prestartId)
        {
            PrestartId = prestartId;
            GetTasksAsync();
        }

        private string taskName;
        public string TaskName
        {
            get { return taskName; }
            set { SetProperty(ref taskName, value, "TaskName");}
        }

        private ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public ObservableCollection<Task> Tasks
        {
            get { return tasks; }
            set { SetProperty(ref tasks, value, "Tasks");}
        }

        public async void GetTasksAsync()
        {
            var table = await App.CloudService.GetTableAsync<Task>();
            var items = await table.ReadAllItemsAsync();
            Tasks.Clear();
            foreach (var item in items)
            {
                if (item.PrestartId == PrestartId)
                {
                    Tasks.Add(item);
                }
            }
        }

        Command newCommand;

        public Command NewCommand
            => newCommand ?? (newCommand = new Command(async () => await ExecuteNewCommand()));

        async System.Threading.Tasks.Task ExecuteNewCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var table = await App.CloudService.GetTableAsync<Task>();
                var task = await table.CreateItemAsync(new Task {Description = taskName, PrestartId = PrestartId});
                Tasks.Add(task);
                await App.CloudService.SyncOfflineCacheAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new TaskDetailPage(task));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command deleteCommand;

        public Command DeleteCommand
            => deleteCommand ?? (deleteCommand = new Command<string>(async (string id) => await ExecuteDeleteCommand(id)));

        async System.Threading.Tasks.Task ExecuteDeleteCommand(String id)
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                var table = await App.CloudService.GetTableAsync<Task>();
                var task = await table.ReadItemAsync(id);
                await table.DeleteItemAsync(task);
                GetTasksAsync();
                await App.CloudService.SyncOfflineCacheAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
