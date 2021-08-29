using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DekanatFPM.Models
{
    public class Statement
    {
        public int StatementID { get; set; }
        public int SubjectID { get; set; }
        public int StudentID { get; set; }
        public bool Repass { get; set; }
        public int Grade { get; set; }
        public int Semester { get; set; }
        public Subject Subject { get; set; }
        public Student Student { get; set; }
    }
}