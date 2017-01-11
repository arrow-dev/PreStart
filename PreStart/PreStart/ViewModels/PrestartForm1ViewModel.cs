using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm1ViewModel : BaseViewModel 
    {
        
        public Prestart Prestart { get; set; }

        public ObservableCollection<Site> Sites { get; set; }

        public PrestartForm1ViewModel()
        {
            Prestart = new Prestart();
            LoadSites();
        }

        private void LoadSites()
        {
            //Temporary, need to hook up to db!
            Sites = new ObservableCollection<Site>
            {
                new Site {Name = "Cycleway", Id = "01de9f41-d2df-4ef9-8465-bfa8487d4c60" },
                new Site {Name = "Motorway", Id = "39b2418a-07b5-4740-9464-3eb2a6a4e342" },
                new Site {Name = "Quarry", Id = "518a6a69-b5eb-4f35-867c-15509db3aad4" },
                new Site {Name = "Northern Intersection", Id = "65d67dfe-40d1-4a63-9ac0-0d7428501d53" },
                new Site {Name = "Stadium", Id = "9e36e726-c7d7-49ab-9af5-412fe66f9404" },
                new Site {Name = "Yard", Id = "dc4a8495-3a2d-49d2-ae7c-16157ee7e20e" },
            };
        }

        public PrestartForm1ViewModel(Prestart prestart)
        {
            Prestart = prestart;
        }

        Command preFillCommand;

        public Command PreFillCommand
            => preFillCommand ?? (preFillCommand = new Command(async () => await ExecutePreFillCommand()));

        async Task ExecutePreFillCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {
                //Get yesterday's form and prefill some properties
                var table = await App.CloudService.GetTableAsync<Prestart>();
                var items = await table.ReadAllItemsAsync();
                items.OrderByDescending(I => I.CreatedAt);
                var item = items.Last();
                Prestart.Department = item.Department;
                Prestart.ContractName = item.ContractName;
                Prestart.ContractNumber = item.ContractNumber;
                Prestart.Location = item.Location;
                Prestart.LotNo = item.LotNo;
                Prestart.SiteId = item.SiteId;
                Prestart.JobNumber = item.JobNumber;
                Prestart.TmpNumber = item.TmpNumber;
                Prestart.SiteFirstAider = item.SiteFirstAider;
                Prestart.CertificateNumber = item.CertificateNumber;
                Prestart.QuarryManager = item.QuarryManager;
                Prestart.SiteManager = item.SiteManager;
                Prestart.StmsNumber = item.StmsNumber;
                

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        Command nextCommand;

        public Command NextCommand
            => nextCommand ?? (nextCommand = new Command(async () => await ExecuteNextCommand()));

        async Task ExecuteNextCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;

            try
            {

               
                await Application.Current.MainPage.Navigation.PushAsync(new PrestartForm2(Prestart));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private static DateTime dayTime = DateTime.Now;

        private static string fixdate(DateTime a)
        {
           
            string temp = String.Format("{0:d/M/yyyy}", a);
            return temp;
        }

        private string dateString = fixdate(dayTime);

        public string DateString
        {
            set
            {
                if (dateString != value)
                {
                    dateString = value;
                    OnPropertyChanged("DateString");
                }
            }
            get
            {
               
                return dateString;
                
            }

            
        }


    
    }
}
