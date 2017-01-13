using PreStart.Abstractions;
using PropertyChanged;
namespace PreStart.Models
{
    [ImplementPropertyChanged]
    public class Hazard : TableData
    {
        public string Description { get; set; }

        public string Repercussion { get; set; }

        public string RiskBefore { get; set; }

        public string Response { get; set; }

        public string RiskAfter { get; set; }

        public string TaskId { get; set; }
    }
}
