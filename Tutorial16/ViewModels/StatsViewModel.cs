using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tutorial16.ViewModels
{
    public class StatsViewModel
    {
        public int TotalNumber { get; set; }
        public int NumberMale { get; set; }
        public int NumberFemale { get; set; }
        public double AverageAge { get; set; }
        public int Youngest { get; set; }
        public int Oldest { get; set; }
        public int CombinedAge { get; set; }

    }
}