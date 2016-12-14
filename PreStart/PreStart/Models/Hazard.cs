using PreStart.Abstractions;
using PropertyChanged;
namespace PreStart.Models
{
    [ImplementPropertyChanged]
    public class Hazard : TableData
    {
        public string Description { get; set; }

        public string Repercussion { get; set; }

        public int RiskBefore { get; set; }

        public string Response { get; set; }

        public int RiskAfter { get; set; }

        public string TaskId { get; set; }
    }
}
