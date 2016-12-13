using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HazardDetailViewPage : ContentPage
    {
        public HazardDetailViewPage()
        {
            InitializeComponent();

            StepperRiskBefore.ValueChanged += OnStepperRiskBeforeValueChanged;
            StepperRiskAfter.ValueChanged += OnStepperRiskAfterValueChanged;

            
        }

        private void OnStepperRiskAfterValueChanged(object sender, ValueChangedEventArgs e)
        {
            LabelRiskAfter.Text = StepperRiskAfter.Value.ToString();
        }

        private void OnStepperRiskBeforeValueChanged(object sender, ValueChangedEventArgs e)
        {
          LabelRiskBefore.Text = StepperRiskBefore.Value.ToString();
        }
    }
}
