using System;
using System.Diagnostics;
using PreStart.Abstractions;
using PreStart.Models;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    public class HazardDetailViewModel : BaseViewModel
    {  
        public Hazard Hazard { get; set; }

        public HazardDetailViewModel(Hazard hazard)
        {
            this.Hazard = hazard;

        }
        //Edit Command
        Command editCommand;
       
        public Command EditCommand
            => editCommand ?? (editCommand = new Command(async () => await ExecuteEditCommand()));

        async System.Threading.Tasks.Task ExecuteEditCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                //Implementation goes here
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
