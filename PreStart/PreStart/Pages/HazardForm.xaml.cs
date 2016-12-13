using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HazardForm : ContentPage
    {
        public HazardForm()
        {
            InitializeComponent();

            StepperRiskBefore.ValueChanged += OnStepperRiskBeforeChanged;
            StepperRiskAfter.ValueChanged += OnStepperRiskAfterChanged;
        }

        private void OnStepperRiskAfterChanged(object sender, ValueChangedEventArgs e)
        {
            LabelRiskAfter.Text = StepperRiskAfter.Value.ToString();
        }

        private void OnStepperRiskBeforeChanged(object sender, ValueChangedEventArgs e)
        {
            LabelRiskBefore.Text = StepperRiskBefore.Value.ToString();
        }
    }
}
