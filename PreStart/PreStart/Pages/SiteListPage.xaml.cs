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
            BindingContext = new SiteListPageViewModel(Navigation);
            listview.ItemTapped += Listview_OnItemTapped;
        }



        void Listview_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = (Location)e.Item;
            DisplayAlert("Location Selected", item.Name + " is now your default location.", "OK");
        }
    }
}
