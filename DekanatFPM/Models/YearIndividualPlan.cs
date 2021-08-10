using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DekanatFPM.Models
{
    public class YearIndividualPlan
    {
        [Key]
        [ForeignKey("Student")]
        public int StudentID { get; set; }
        public int Year { get; set; }
        public Student Student { get; set; }
        public YearIndividualPlan()
        {

        }
        public YearIndividualPlan(YearIndividualPlan plan)
        {
            StudentID = plan.StudentID;
            Year = plan.Year;
            Student = plan.Student;
        }
    }
}