using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatFPM.Models
{
    public class Subject
    {
        public int SubjectID { get; set; }
        public string Name { get; set; }
        public int GroupID { get; set; }
        public int Year { get; set; }
        public int? ControlExam { get; set; }
        public int? ControlCredit { get; set; }
        public int? ControlCourseWork { get; set; }
        public int? ControlIndividual { get; set; }
        public Group Group { get; set; }

    }
}