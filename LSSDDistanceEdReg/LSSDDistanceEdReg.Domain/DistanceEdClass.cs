using System;
using System.Collections.Generic;
using System.Text;

namespace LSSD.DistanceEdReg
{
    public class DistanceEdClass
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }
        public string MoreInfoURL { get; set; }

        // Are the teaching materials available for teachers to request, without 
        // having students in a class?
        public bool MaterialsAvailableToTeachers { get; set; }

        public bool MentorTeacherRequired { get; set; }
    }
}
