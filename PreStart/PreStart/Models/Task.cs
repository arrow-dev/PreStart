using PreStart.Abstractions;
using PropertyChanged;
namespace PreStart.Models
{
    [ImplementPropertyChanged]
    public class Task : TableData
    {
        public string Description { get; set; }
        public string PrestartId { get; set; }
    }
}
