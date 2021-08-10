using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DekanatFPM.Models
{
    public class Group
    {
        public int GroupID { get; set; }
        public int SpecializationID { get; set; }

        [Range(2000, 2099, ErrorMessage ="Допустимі значення 2000-2099")]
        public int StartYear { get; set; }
        public TypeGroup Type { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string YearsIndividualPlans { get; set; }
        public Specialization Specialization{ get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public Group()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();
            YearsIndividualPlans = string.Empty;
        }
    }
}
