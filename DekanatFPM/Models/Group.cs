using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public int SpecializationID { get; set; }
        public int Type { get; set; }
        public Specialization Specialization{ get; set; }
        public ICollection<Student> Students { get; set; }
    }
}
