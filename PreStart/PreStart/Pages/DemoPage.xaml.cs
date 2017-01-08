using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.Models;
using PreStart.ViewModels;
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

            if (listitem != null)
            {
                Page detailVar = null;

                switch (listitem.Title)
                {
                    case "Prestart meeting":
                        detailVar = (PrestartForm1) Activator.CreateInstance(listitem.TargetType);
                        break;
                    case "Task":
                        var table = await App.CloudService.GetTableAsync<Prestart>();
                        var items = await table.ReadAllItemsAsync();
                        items.OrderByDescending(I => I.CreatedAt);
                        var item = items.Last();
                        detailVar = (TaskManagerPage)Activator.CreateInstance(listitem.TargetType, item.Id);
                        break;
                    
                }
                
                Detail = detailVar;

                
                masterPage.ListView.SelectedItem = null;
                IsPresented = false;

            }

        }
    }
}
