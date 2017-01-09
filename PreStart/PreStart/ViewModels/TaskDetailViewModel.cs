using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class TaskDetailViewModel : BaseViewModel
    {
        public Task Task;

        public TaskDetailViewModel(Task task)
        {
            Task = task;
        }

        private ObservableCollection<Hazard> hazards = new ObservableCollection<Hazard>();

        public ObservableCollection<Hazard> Hazards
        {
            get { return hazards; }
            set { SetProperty(ref hazards, value, "Hazards"); }
        }
 

        public async void GetHazardsAsync()
        {
            var table = await App.CloudService.GetTableAsync<Hazard>();
            var items = await table.ReadAllItemsAsync();
            Hazards.Clear();
            foreach (var aHazard in items)
            {
                if (aHazard.TaskId == Task.Id)
                {
                    Hazards.Add(aHazard);
                }
            }
        }



        private Command addHazard;

        public Command AddHazard
            => addHazard ?? (addHazard = new Command(async () => await ExecuteAddHazard()));

        async System.Threading.Tasks.Task ExecuteAddHazard()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new HazardForm(new Hazard {TaskId = Task.Id}));
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


        private Command deleteCommand;

        public Command DeleteCommand
            =>
            deleteCommand ?? (deleteCommand = new Command<string>(async (string id) => await ExecuteDeleteHazard(id)));

        async System.Threading.Tasks.Task ExecuteDeleteHazard(String id)
        {
            if(IsBusy)
                return;
            IsBusy = true;

            try
            {
                var hazard_table = await App.CloudService.GetTableAsync<Hazard>();
                var aHazard = await hazard_table.ReadItemAsync(id);
                await hazard_table.DeleteItemAsync(aHazard);
                GetHazardsAsync();
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
