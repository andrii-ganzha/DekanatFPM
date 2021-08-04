using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class Specialty
    {
        public int SpecialtyID { get; set; }
        public string Name { get; set; }
        public int TrainingDirectionID { get; set; }
        public TrainingDirection TrainingDirection { get; set; }
        public ICollection<Specialization> Specializations { get; set; }
    }
}
