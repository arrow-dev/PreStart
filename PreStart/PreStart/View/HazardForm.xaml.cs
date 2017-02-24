using Prestart.Model;
using Prestart.ViewModel;
using System;
using Xamarin.Forms;

namespace Prestart.View
{
    public partial class HazardForm : ContentPage
    {
        private int StepValue;

        public HazardForm()
        {
            InitializeComponent();
            BindingContext = new HazardFormViewModel(Navigation);
            RBSliderPCL.ValueChanged += RBPclOnSliderChanged;
            RBSliderPLL.ValueChanged += RBPllOnSliderChanged;
            RASliderPCL.ValueChanged += RAPclOnSliderChanged;
            RASliderPLL.ValueChanged += RAPllOnSliderChanged;
        }

        public HazardForm(Hazard hazard)
        {
            InitializeComponent();
            BindingContext = new HazardFormViewModel(Navigation, hazard);
            RBSliderPCL.ValueChanged += RBPclOnSliderChanged;
            RBSliderPLL.ValueChanged += RBPllOnSliderChanged;
            RASliderPCL.ValueChanged += RAPclOnSliderChanged;
            RASliderPLL.ValueChanged += RAPllOnSliderChanged;
        }

        private void RiskBeforeBtn_OnClick(object sender, EventArgs e)
        {
            if (RiskBeforeBtn.Text == "RATE")
            {
                RiskBeforeBtn.Text = "HIDE";
                riskbeforeslider.IsVisible = true;
            }
            else
            {
                RiskBeforeBtn.Text = "RATE";
                riskbeforeslider.IsVisible = false;
            }
        }

        private void RiskAfterBtn_OnClick(object sender, EventArgs e)
        {
            if (RiskAfterBtn.Text == "RATE")
            {
                RiskAfterBtn.Text = "HIDE";
                riskafterslider.IsVisible = true;
            }
            else
            {
                RiskAfterBtn.Text = "RATE";
                riskafterslider.IsVisible = false;
            }
        }

        //PCL label switch
        private void PCLSwitch(int a, Label risklabel)
        {
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
        }

        //PLL label switch
        private void PLLSwitch(int a, Label risklabel)
        {
            switch (a)
            {
                case 1:
                    risklabel.Text = "Rare";
                    break;
                case 2:
                    risklabel.Text = "Unlikely";
                    break;
                case 3:
                    risklabel.Text = "Possible";
                    break;
                case 4:
                    risklabel.Text = "Likely";
                    break;
                case 5:
                    risklabel.Text = "Almost Certain";
                    break;
            }
        }

        private void OnSliderChanged(ValueChangedEventArgs e, Slider slider1, Slider slider2, Label siderlabel, Label risklabel)
        {
            StepValue = 1;
            var newStep = Math.Round(e.NewValue / StepValue);
            slider1.Value = newStep * StepValue;
            int a = Convert.ToInt32(e.NewValue);
            //check if the label is PCL or PLL
            //apply different label switch
            if (risklabel == RAPCL || risklabel == RBPCL)
            {
                PCLSwitch(a, risklabel);
            }
            else
            {
                PLLSwitch(a, risklabel);
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
                //label.BackgroundColor = Color.Green;
                label.TextColor = Color.White;
                label.Text = "LOW";
            }

            if (a >= 7 && a <= 15)
            {
                //label.BackgroundColor = Color.Yellow;
                label.TextColor = Color.Gray;
                label.Text = "MEDIUM";
            }

            if (a >= 16 && a <= 22)
            {
                label.TextColor = Color.White;
                //label.BackgroundColor = Color.Red;
                label.Text = "HIGH";
            }

            if (a >= 23 && a <= 25)
            {
                //label.BackgroundColor = Color.Black;
                label.TextColor = Color.White;
                label.Text = "EXTREME";
            }

        }
    }
}
