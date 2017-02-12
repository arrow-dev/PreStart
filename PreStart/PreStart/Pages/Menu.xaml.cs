
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class Menu : MasterDetailPage
    {
        public ListView ListView { get { return ButtonList; } }
        public Menu()
        {
            InitializeComponent();
            Detail = new PrestartNavigationPage(new HomePage());

            var buttons = new ObservableCollection<string> { "Meeting Room", "Hazard Register/Sign On", "Log Book" };
            ButtonList.SetBinding(ListView.ItemsSourceProperty, new Binding("."));
            ButtonList.BindingContext = buttons;
            ButtonList.ItemSelected += ButtonList_OnItemSelected;
            
        }

        private async void ButtonList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var demopage = new NavigationPage(new TabbedPage());
            var carouselpage = new NavigationPage(new CarouselPage());

            if (e.SelectedItem == null)
            {
                return;
            }
            if (e.SelectedItem.ToString() == "Hazard Register/Sign On")
            {
                Detail = carouselpage;
                IsPresented = false;
            }
            else if (e.SelectedItem.ToString() == "Meeting Room")
            {
                var action = await DisplayActionSheet("Start Meeting:", "Cancel", null, "Prestart Form", "Hazard Form");
                switch (action)
                {
                    case "Prestart Form":
                        Detail = new NavigationPage(new PrestartForm1());
                        IsPresented = false;
                        break;
                    case "Hazard Form":
                        Detail = new NavigationPage(new HazardForm());
                        IsPresented = false;
                        break;
                }
            }
            else 
            {
                Detail = demopage;
                IsPresented = false;
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}
