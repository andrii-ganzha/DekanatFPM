using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public int RecordBook { get; set; }
        public int GroupID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public bool PlanWithGroup { get; set; }
        public string Gender { get; set; }
        public Group Group { get; set; }
        public virtual ICollection<YearIndividualPlan> YearIndividualPlans { get; set; }
        public virtual ICollection<Statement> Statements { get; set; }
    }
}