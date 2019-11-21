using System;
using System.Collections.Generic;
using System.Text;

namespace LSSD.DistanceEdReg
{
    class DistanceEdClass
    {
        public string Name { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }

        // Are the teaching materials available for teachers to request, without 
        // having students in a class?
        public bool MaterialsAvailableToTeachers { get; set; }
    }
}
