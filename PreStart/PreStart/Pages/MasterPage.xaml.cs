using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class MasterPage : ContentPage
    {
        public ListView ListView { get { return listView; } } 
        

        public MasterPage()

        {

            InitializeComponent();


              
            var masterPageItems = new List<MasterPageItem>();

            masterPageItems.Add(new MasterPageItem
            {

                Title = "Prestart meeting",
                
                TargetType = typeof(PrestartForm1)


            });

            masterPageItems.Add(new MasterPageItem
            {

                Title = "Task",

                TargetType = typeof(TaskManagerPage)


            });

            masterPageItems.Add(new MasterPageItem
            {

                Title = "Hazard",

                TargetType = typeof(HazardDetailViewModel)


            });






            ListView.ItemsSource = masterPageItems;

        }
    }
}
