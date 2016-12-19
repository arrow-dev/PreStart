using PreStart.Abstractions;
using PreStart.Models;
using PreStart.Pages;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Task = System.Threading.Tasks.Task;

namespace PreStart.ViewModels
{
    public class PrestartForm1ViewModel : BaseViewModel
    {
        
        public Prestart Prestart { get; set; }
        

        public PrestartForm1ViewModel()
        {
            Prestart = new Prestart();
            
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
                Prestart.Project = item.Project;
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
