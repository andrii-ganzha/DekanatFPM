using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatFPM.Models
{
    public class YearIndividualPlan
    {
        public int YearIndividualPlanID { get; set; }
        public int Year { get; set; }
        public List<Subject> Subjects { get; set; }

    }
}