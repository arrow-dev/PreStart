using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PreStart.Models;
using PreStart.ViewModels;
using Xamarin.Forms;

namespace PreStart.Pages
{
    public partial class HazardForm : ContentPage
    {
        public HazardForm(Hazard hazard)
        {
            InitializeComponent();
            BindingContext = new HazardFormViewModel(hazard);
            SliderRiskBefore.ValueChanged += OnSliderRiskBeforeChanged;
            SliderRiskAfter.ValueChanged += OnSliderRiskAfterChanged;

           
        }

        private void OnSliderRiskAfterChanged(object sender, ValueChangedEventArgs e)
        {
            SetRiskLabelColor(LabelRiskAfter, SliderRiskAfter);
        }

        private void OnSliderRiskBeforeChanged(object sender, ValueChangedEventArgs e)
        {
           SetRiskLabelColor(LabelRiskBefore, SliderRiskBefore);



        }

        private void SetRiskLabelColor(Label label, Slider slider)
        {



            if (slider.Value < 7)
            {
                label.BackgroundColor = Color.Green;
                label.TextColor = Color.Black;
                label.Text = "Low " + slider.Value;
            }

            if (slider.Value >= 7 && slider.Value <= 15)
            {
                label.BackgroundColor = Color.Yellow;
                label.TextColor = Color.Black;
                label.Text = "Med " + slider.Value;
            }

            if (slider.Value >= 16 && slider.Value <= 22)
            {
                label.TextColor = Color.Black;
                label.BackgroundColor = Color.Red;
                label.Text = "High " + slider.Value;
            }

            if (slider.Value >= 23 && slider.Value <= 25)
            {
                label.BackgroundColor = Color.Black;
                label.TextColor = Color.White;
                label.Text = "Ext " + slider.Value;
            }

        }
               



    }
}
