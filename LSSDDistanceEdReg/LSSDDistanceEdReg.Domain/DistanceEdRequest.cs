﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LSSD.DistanceEdReg
{
    public class DistanceEdRequest
    {
        public string StudentName { get; set; }
        public string StudentNumber { get; set; }
        public string StudentBaseSchool { get; set; }
        public string Comments { get; set; }        
        public int CourseID { get; set; }
        public string MentorTeacherName { get; set; }
        public string Requestor { get; set; }
        public DateTime DateRequested { get; set; }
    }
}