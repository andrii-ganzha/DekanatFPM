using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class TrainingDirection
    {
        public int TrainingDirectionID { get; set; }
        public string Name { get; set; }
        public ICollection<Specialty> Specialties { get; set; }
    }
}
