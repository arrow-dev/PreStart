using System;
using PreStart.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace PreStart.Models
{
    [ImplementPropertyChanged]
    public class Prestart : TableData 
    {
        public string ContractNumber { get; set; }

        public string ContractName { get; set; }

        public string Location { get; set; }

        public string LotNo { get; set; }

        public string Department { get; set; }

        public string Project { get; set; }

        public string JobNumber { get; set; }

        public string SiteManager { get; set; }

        public string StmsNumber { get; set; }

        public string TmpNumber { get; set; }

        public string SiteFirstAider { get; set; }

        public string QuarryManager { get; set; }

        public string CertificateNumber { get; set; }

        public string EmergencyPlan { get; set; }

        public string Plan { get; set; }

        public string Doing { get; set; }

        public string PlantEquipment { get; set; }

        public string SpecialRequirements { get; set; }

        public string HoldPoints { get; set; }

        public string QualityChecks { get; set; }

        public string Problems { get; set; }

        public string Improvements { get; set; }

        public string Differences { get; set; }

        public string MinimumProductivityRequirements { get; set; }

        public bool FitForWork { get; set; }

        public bool SiteSecure { get; set; }

        public bool BarricadesRequired { get; set; }

        public bool SuitableAccess { get; set; }

        public bool WeatherForecastConsidered { get; set; }

        public bool SpecialistPpeRequired { get; set; }

        public bool ErosionAndSedimentControlImplemented { get; set; }

        public static implicit operator Page(Prestart v)
        {
            throw new NotImplementedException();
        }
    }
}
