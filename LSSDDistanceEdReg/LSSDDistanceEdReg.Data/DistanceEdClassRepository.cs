using LSSD.DistanceEdReg;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSSD.DistanceEdReg.Data
{
    public class DistanceEdClassRepository
    {

        public List<DistanceEdClass> GetAvailableClasses(DateTime ReferenceDate)
        {
            return new List<DistanceEdClass>()
            {
                new DistanceEdClass()
                {
                    ID = 1,
                    Name = "Calculus 30 (SCHEDULED)",
                    AvailableFrom = new DateTime(2019, 09, 02),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    MoreInfoURL = @"https://lskysd.scholantistest.com/apps/pages/index.jsp?uREC_ID=1091153&type=d&pREC_ID=1367702",
                    Description = "Calculus 30 is taught via video teleconference by Mr. Shaun Ross, daily at 12:45pm, during Semester 2 only. Mentor teacher at student's base school required. Textbook is Calculus 30 by Burt Thiessen."
                },
                new DistanceEdClass()
                {
                    ID = 2,
                    Name = "Physics 30 (SCHEDULED)",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Physics 30 is taught via video teleconference by Mr. Shaun Ross, daily at 10:00am, during semester 2 only. Mentor teacher at student's base school required. Textbook is Pearson Physics 30 ISBN-13: 978-0-13-505048-4."
                },
                new DistanceEdClass()
                {
                    ID = 3,
                    Name = "Computer Science 20 - Semester 1",
                    AvailableFrom = new DateTime(2019, 09, 02),
                    AvailableTo = new DateTime(2020, 06, 30),
                    Description = "Asynchronous course, all assessment done by Mr. Shawn Whyte at UCHS."
                },
                new DistanceEdClass()
                {
                    ID = 4,
                    Name = "Computer Science 20 - Semester 2",
                    AvailableFrom = new DateTime(2019, 09, 02),
                    AvailableTo = new DateTime(2020, 06, 30),
                    Description = "Asynchronous course, all assessment done by Mr. Shawn Whyte at UCHS."
                },
                new DistanceEdClass()
                {
                    ID = 5,
                    Name = "Computer Science 20 - Full year",
                    AvailableFrom = new DateTime(2019, 09, 02),
                    AvailableTo = new DateTime(2020, 06, 30),
                    Description = "Asynchronous course, all assessment done by Mr. Shawn Whyte at UCHS."
                },
                new DistanceEdClass()
                {
                    ID = 6,
                    Name = "Computer Science 30 - Semester 1",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    Description = "Asynchronous course, all assessment done by Mr. Shawn Whyte at UCHS."
                },
                new DistanceEdClass()
                {
                    ID = 7,
                    Name = "Computer Science 30 - Semester 2",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    Description = "Asynchronous course, all assessment done by Mr. Shawn Whyte at UCHS."
                },
                new DistanceEdClass()
                {
                    ID = 8,
                    Name = "Computer Science 30 - Full year",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    Description = "Asynchronous course, all assessment done by Mr. Shawn Whyte at UCHS."
                },
                new DistanceEdClass()
                {
                    ID = 9,
                    Name = "Physchology 20 - Semester 1",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 10,
                    Name = "Psychology 20 - Semester 2",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 11,
                    Name = "Physchology 30 - Semester 2",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 12,
                    Name = "CWE A30 - Semester 1",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 13,
                    Name = "CWE A30 - Semester 2",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 14,
                    Name = "CWE A30 - Full Year",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 15,
                    Name = "Brilliance Credits - Semester 1",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    MoreInfoURL = @"https://portal.lskysd.ca/my/personal/ruth_weber/_layouts/15/WopiFrame.aspx?sourcedoc=/my/personal/ruth_weber/Documents/Discover%20Your%20Brilliance.docx&action=default",
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 16,
                    Name = "Brilliance Credits - Semester 2",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    MoreInfoURL = @"https://portal.lskysd.ca/my/personal/ruth_weber/_layouts/15/WopiFrame.aspx?sourcedoc=/my/personal/ruth_weber/Documents/Discover%20Your%20Brilliance.docx&action=default",
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 17,
                    Name = "Brilliance Credits - Full Year",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    MoreInfoURL = @"https://portal.lskysd.ca/my/personal/ruth_weber/_layouts/15/WopiFrame.aspx?sourcedoc=/my/personal/ruth_weber/Documents/Discover%20Your%20Brilliance.docx&action=default",
                    Description = "Mentor teacher at student's base school required. Rubrics and quizzes are provided. Your school determines class time and schedule."
                },
                new DistanceEdClass()
                {
                    ID = 18,
                    Name = "PAA Module: Robotics and Automation",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "30 Hour module suitable for a PAA survey class. PAA teacher required. Assessment rubrics provided."
                },
                new DistanceEdClass()
                {
                    ID = 19,
                    Name = "PAA Module: Communications Media Streaming",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "30 Hour module suitable for a PAA survey class. PAA teacher required. Assessment rubrics provided."
                },
                new DistanceEdClass()
                {
                    ID = 20,
                    Name = "PAA Module: 3D Printing and Design",
                    AvailableFrom = new DateTime(2020, 02, 03),
                    AvailableTo = new DateTime(2020, 06, 30),
                    MentorTeacherRequired = true,
                    Description = "30 Hour module suitable for a PAA survey class. PAA teacher required. Assessment rubrics provided."
                },
            };
        }
    }
}
