using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private int StepValue;

        public Hazard Hazard;
        public HazardForm(Hazard hazard)
        {
            InitializeComponent();
            Hazard = hazard;
            BindingContext = new HazardFormViewModel(Hazard);
            RBSliderPCL.ValueChanged += RBPclOnSliderChanged;
            RBSliderPLL.ValueChanged += RBPllOnSliderChanged;
            RASliderPCL.ValueChanged += RAPclOnSliderChanged;
            RASliderPLL.ValueChanged += RAPllOnSliderChanged;
            
        }

        

        private void RiskBeforeBtn_OnClick(object sender, EventArgs e)
        {
            if (RiskBeforeBtn.Text == "+")
            {
                RiskBeforeBtn.Text = "-";
                riskbeforeslider.IsVisible = true;
            }
            else
            {
                RiskBeforeBtn.Text = "+";
                riskbeforeslider.IsVisible = false;
            }
        }

        private void RiskAfterBtn_OnClick(object sender, EventArgs e)
        {
            if (RiskAfterBtn.Text == "+")
            {
                RiskAfterBtn.Text = "-";
                riskafterslider.IsVisible = true;
            }
            else
            {
                RiskAfterBtn.Text = "+";
                riskafterslider.IsVisible = false;
            }
        }


        private void OnSliderChanged(ValueChangedEventArgs e, Slider slider1, Slider slider2, Label siderlabel, Label risklabel)
        {
            StepValue = 1;
            var newStep = Math.Round(e.NewValue / StepValue);
            slider1.Value = newStep * StepValue;
            int a = Convert.ToInt32(e.NewValue);
            switch (a)
            {
                case 1:
                    risklabel.Text = "Insignificant";
                    break;
                case 2:
                    risklabel.Text = "Minor";
                    break;
                case 3:
                    risklabel.Text = "Significant";
                    break;
                case 4:
                    risklabel.Text = "Major";
                    break;
                case 5:
                    risklabel.Text = "Catastrophic";
                    break;
            }
            SetRiskLabelColor(siderlabel, slider1, slider2);
        }

        private void RBPclOnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            OnSliderChanged(e, RBSliderPCL, RBSliderPLL, LabelRiskBefore, RBPCL);
        }

        private void RBPllOnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            OnSliderChanged(e, RBSliderPLL, RBSliderPCL, LabelRiskBefore, RBPLL);
        }

        private void RAPclOnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            OnSliderChanged(e, RASliderPCL, RASliderPLL, LabelRiskAfter, RAPCL);
        }

        private void RAPllOnSliderChanged(object sender, ValueChangedEventArgs e)
        {
            OnSliderChanged(e, RASliderPLL, RBSliderPCL, LabelRiskAfter, RAPLL);
        }

        private int RiskCalculator(int pll, int pcl)
        {
            var score = 0;

            if (pcl == 1)
            {
                switch (pll)
                {
                    case 1:
                        score = 1;
                        break;
                    case 2:
                        score = 2;
                        break;
                    case 3:
                        score = 4;
                        break;
                    case 4:
                        score = 7;
                        break;
                    case 5:
                        score = 11;
                        break;
                }
            }
            if (pcl == 2)
            {
                switch (pll)
                {
                    case 1:
                        score = 3;
                        break;
                    case 2:
                        score = 5;
                        break;
                    case 3:
                        score = 8;
                        break;
                    case 4:
                        score = 12;
                        break;
                    case 5:
                        score = 16;
                        break;
                }
            }
            if (pcl == 3)
            {
                switch (pll)
                {
                    case 1:
                        score = 6;
                        break;
                    case 2:
                        score = 9;
                        break;
                    case 3:
                        score = 13;
                        break;
                    case 4:
                        score = 17;
                        break;
                    case 5:
                        score = 20;
                        break;
                }
            }
            if (pcl == 4)
            {
                switch (pll)
                {
                    case 1:
                        score = 10;
                        break;
                    case 2:
                        score = 14;
                        break;
                    case 3:
                        score = 18;
                        break;
                    case 4:
                        score = 21;
                        break;
                    case 5:
                        score = 23;
                        break;
                }
            }
            if (pcl == 5)
            {
                switch (pll)
                {
                    case 1:
                        score = 15;
                        break;
                    case 2:
                        score = 19;
                        break;
                    case 3:
                        score = 22;
                        break;
                    case 4:
                        score = 24;
                        break;
                    case 5:
                        score = 25;
                        break;
                }
            }

            return score;

        }

        private void SetRiskLabelColor(Label label, Slider slider1, Slider slider2)
        {
            int pll = Convert.ToInt32(slider1.Value);
            int pcl = Convert.ToInt32(slider2.Value);
            int a = RiskCalculator(pll, pcl);


            if (a < 7)
            {
                label.BackgroundColor = Color.Green;
                label.TextColor = Color.Black;
                label.Text = "Low " + a;
            }

            if (a >= 7 && a <= 15)
            {
                label.BackgroundColor = Color.Yellow;
                label.TextColor = Color.Black;
                label.Text = "Med " + a;
            }

            if (a >= 16 && a <= 22)
            {
                label.TextColor = Color.Black;
                label.BackgroundColor = Color.Red;
                label.Text = "High " + a;
            }

            if (a >= 23 && a <= 25)
            {
                label.BackgroundColor = Color.Black;
                label.TextColor = Color.White;
                label.Text = "Ext " + a;
            }

        }



    }
}
