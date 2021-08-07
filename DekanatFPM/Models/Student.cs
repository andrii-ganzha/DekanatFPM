using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public int GroupID { get; set; }
        public int YearIndividualPlanID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public string Gender { get; set; }
        public Group Group { get; set; }
        public YearIndividualPlan YearIndividualPlan { get; set; }

    }
}