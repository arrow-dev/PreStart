using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public  class PrestartDetailViewModel : BaseViewModel
    {
        private Prestart preStart;

        public Prestart Prestart
        {
            get { return preStart; }
            set { SetProperty(ref preStart,value,"Prestart");}
        }
        

        public PrestartDetailViewModel(INavigation nav) : base(nav)
        {
            GetLastestPrestart();
        }


        public PrestartDetailViewModel(Prestart prestart, INavigation nav) : base(nav)
        {

            Prestart = prestart;
        }


        
        
        public async void GetLastestPrestart()
        {
            //await  App.CloudService.SyncOfflineCacheAsync();
            var table = await App.CloudService.GetTableAsync<Prestart>();
            var items = await table.ReadAllItemsAsync();
            if (items.Count == 0)
            {
                
                var action = await App.Current.MainPage.DisplayActionSheet("No Prestart Meeting Record Found!", "Cancel", null, "Back to Main Page", "New Prestart Form");
                switch (action)
                {
                    case "Back to Main Page":
                        
                        await Navigation.PushAsync(new HomePage());
                        break;
                    case "New Prestart Form":
                       
                        await Navigation.PushAsync(new PrestartForm1());
                        break;
                }
                return;
            }
            items.OrderByDescending(I => I.CreatedAt);
            Prestart item = items.Last();
            Prestart = item;
        }

    }
}
