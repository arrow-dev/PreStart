using System;
using System.Diagnostics;
using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using Xamarin.Forms;

namespace PreStart.ViewModels
{
    class SignOnDetailPageViewModel : BaseViewModel
    {
        public SignOn SignOn { get; set; }

        public SignOnDetailPageViewModel(SignOn signOn, INavigation navigation) : base(navigation)
        {
            SignOn = signOn;
        }

        //Edit Command
        //Command editCommand;

        //public Command EditCommand
        //    => editCommand ?? (editCommand = new Command(async () => await ExecuteEditCommand()));

        //async System.Threading.Tasks.Task ExecuteEditCommand()
        //{
        //    if (IsBusy)
        //        return;
        //    IsBusy = true;
        //    try
        //    {
        //        await Navigation.PushAsync(new SignaturePage(SignOn));
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"{ex.Message}");
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}
    }
}
