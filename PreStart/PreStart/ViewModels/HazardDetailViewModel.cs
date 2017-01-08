using PreStart.Abstractions;
using PreStart.Models;

namespace PreStart.ViewModels
{
    public class HazardDetailViewModel : BaseViewModel
    {  
        public Hazard Hazard { get; set; }

        public HazardDetailViewModel(Hazard hazard)
        {
            this.Hazard = hazard;

        }
        //Edit Command
    }
}
