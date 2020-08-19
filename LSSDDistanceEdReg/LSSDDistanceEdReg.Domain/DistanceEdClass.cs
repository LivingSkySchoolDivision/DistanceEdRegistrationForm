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
        public DateTime Starts { get; set; }
        public DateTime Ends { get; set; }
        public DateTime RegistrationAvailableFrom { get; set; }
        public DateTime RegistrationAvailableTo { get; set; }
        public string MoreInfoURL { get; set; }
        public string DeliveryMethod { get; set; }
        public string PreRequisites { get; set; }
        public string RequiredMaterials { get; set; }
        public string BlackboardID { get; set; }

        public bool IsRequestable { get; set; }

        // Are the teaching materials available for teachers to request, without 
        // having students in a class?
        public bool MaterialsAvailableToTeachers { get; set; }

        public bool MentorTeacherRequired { get; set; }
    }
}
