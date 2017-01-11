using PreStart.Models;
using PreStart.ViewModels;
using System;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class DemoPage : MasterDetailPage
    {

        public DemoPage()
        {
            InitializeComponent();

            masterPage.ListView.ItemSelected += OnItemSelected;
            
        }



        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            var listitem = e.SelectedItem as MasterPageItem;
            var prestartTable = await App.CloudService.GetTableAsync<Prestart>();
            //var hazardTable = await App.CloudService.GetTableAsync<Hazard>();
            var prestartItems = await prestartTable.ReadAllItemsAsync();
            //var hazardItems = await hazardTable.ReadAllItemsAsync();

            if (listitem != null)
            {
                Page detailVar = null;

                switch (listitem.Title)
                {
                    case "Prestart meeting":
                        detailVar = (PrestartForm1) Activator.CreateInstance(listitem.TargetType);
                        break;
                    case "Task":
                        //var table = await App.CloudService.GetTableAsync<Prestart>();
                        //var items = await table.ReadAllItemsAsync();
                        //prestartItems.OrderByDescending(I => I.CreatedAt);
                        //var item = prestartItems.Last();
                        detailVar = (TaskManagerPage)Activator.CreateInstance(listitem.TargetType, Helpers.Settings.DefaultSiteSetting);
                        break;
                    //case "Hazard":
                    //    var h_table = await App.CloudService.GetTableAsync<Hazard>();
                    //    var h_items = await h_table.ReadAllItemsAsync();
                    //    h_items.OrderByDescending(I => I.CreatedAt);
                    //    var i = h_items.Last();
                    //    detailVar = new HazardDetailViewPage(i);
                        //detailVar = (HazardDetailViewPage)Activator.CreateInstance(listitem.TargetType, i);

                        //break;
                }
                
                Detail = detailVar;

                
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;

            }

        }
    }
}
