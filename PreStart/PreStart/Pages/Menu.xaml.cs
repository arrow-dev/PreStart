
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

            var buttons = new ObservableCollection<string> { "Log Book", "Meeting Room" };
            ButtonList.SetBinding(ListView.ItemsSourceProperty, new Binding("."));
            ButtonList.BindingContext = buttons;
            ButtonList.ItemSelected += ButtonList_OnItemSelected;
        }

        private async void ButtonList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var demopage = new NavigationPage(new TabbedPage());

            if (e.SelectedItem == null)
            {
                return;
            }
            if (e.SelectedItem.ToString() == "Meeting Room")
            {
                var action = await DisplayActionSheet("Start Meeting:", "Cancel", null, "1. Prestart Form", "2. Hazard Form", "3. Sign-On Form");
                switch (action)
                {
                    case "1. Prestart Form":
                        Detail = new NavigationPage(new PrestartForm1());
                        break;
                    case "2. Hazard Form":
                        Detail = new NavigationPage(new HazardForm());
                        break;
                    case "3. Sign-On Form":
                        Detail = new NavigationPage(new SignaturePage());
                        break;
                }
            }
            else
            {
                Detail = demopage;
            }
            ((ListView)sender).SelectedItem = null;
        }
    }
}
