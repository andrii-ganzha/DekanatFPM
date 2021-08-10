using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class Specialization
    {
        public int SpecializationID { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int SpecialtyID { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<Group> Groups {get;set;}
    }
}
