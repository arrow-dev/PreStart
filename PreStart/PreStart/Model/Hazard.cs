using System;
using Prestart.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prestart.Model
{
    public class Hazard : TableData, INotifyPropertyChanged
    {

        public string Task { get; set; }

        public string Description { get; set; }

        public string Repercussion { get; set; }
        
        private string _riskbefore = String.Empty;

        public string Response { get; set; }
        
        private string _riskafter = String.Empty;

        public string PrestartId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string RiskBefore
        {
            get { return this._riskbefore; }
            set
            {
                if (value != this._riskbefore)
                {
                    this._riskbefore = value;
                    OnPropertyChanged("RiskBefore");
                }
            }
        }

        public string RiskAfter
        {
            get { return this._riskafter; }
            set
            {
                if (value != this._riskafter)
                {
                    this._riskafter = value;
                    OnPropertyChanged("RiskAfter");
                }
            }
        }
    }
}
