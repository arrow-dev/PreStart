using Prestart.Abstractions;
using Prestart.Model;
using Xamarin.Forms;

namespace Prestart.ViewModel
{
    class HazardDetailViewModel : PrestartBaseViewModel
    {
        Hazard hazard;
        public Hazard Hazard
        {
            get { return hazard; }
            set { SetProperty(ref hazard, value, "Hazard"); }
        }

        public HazardDetailViewModel(INavigation nav, Hazard haz)
        {
            Navigation = nav;
            Hazard = haz;
        }

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
        //        await Navigation.PushAsync(new HazardForm(Hazard));
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
