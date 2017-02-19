﻿using Prestart.Abstractions;

namespace Prestart.Model
{
    public class Hazard : TableData
    {
        public string Task { get; set; }

        public string Description { get; set; }

        public string Repercussion { get; set; }

        public string RiskBefore { get; set; }

        public string Response { get; set; }

        public string RiskAfter { get; set; }

        public string PrestartId { get; set; }
    }
}