using PreStart.Abstractions;
using System;

namespace PreStart.Models
{
    public class Prestart : TableData
    {
        public string Department { get; set; }

        public string Project { get; set; }

        public string JobNumber { get; set; }

        public string SiteManager { get; set; }

        public DateTime Date { get; set; }

        public string StmsNumber { get; set; }

        public string TmpNumber { get; set; }

        public string SiteFirstAider { get; set; }

        public string QuarryManager { get; set; }

        public string CertificateNumber { get; set; }

        public string Plan { get; set; }

        public string Doing { get; set; }

        public string PlantEquipment { get; set; }

        public string SpecialRequirements { get; set; }

        public string HoldPoints { get; set; }

        public string QualityChecks { get; set; }

        public string Problems { get; set; }

        public string MinimumProductivityRequirements { get; set; }

        public bool FitForWork { get; set; }

        public bool SiteSecure { get; set; }

        public bool BarricadesRequired { get; set; }

        public bool SuitableAccess { get; set; }

        public bool WeatherForecastConsidered { get; set; }

        public bool SpecialistPpeRequired { get; set; }

        public bool ErosionAndSedimentControlImplemented { get; set; }
    }
}
