using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;
namespace PreStart.Pages
{
    public partial class SiteListPage : ContentPage
    {
       
        public SiteListPage()
        {
            InitializeComponent();
            BindingContext = new SiteListPageViewModel();
            listview.ItemTapped += Listview_OnItemTapped;
        }



        void Listview_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Location)e.Item;
            DisplayAlert("Site Selected", item.Name + " is your default site.", "OK");
        }
    }
}
