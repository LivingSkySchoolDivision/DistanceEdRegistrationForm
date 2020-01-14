using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSSD.DistanceEdReg;
using LSSD.DistanceEdReg.Data;
using LSSD.DistanceEdReg.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace LSSDDistanceEdReg.WebFrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<string> regErrors = new List<string>();

        private IConfiguration _config;

        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            this._config = configuration;
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostFormSubmit()
        {
            if (ModelState.IsValid)
            {
                regErrors.Clear();

                string studentName = Request.Form["txtStudentName"].ToString();
                string studentNumber = Request.Form["txtStudentNumber"].ToString();
                string studentSchool = Request.Form["txtStudentSchool"].ToString();
                string comments = Request.Form["txtNotes"].ToString();
                string requestor = Request.Form["txtRequestor"].ToString();

                string mentorName = Request.Form["txtMentor"].ToString() ?? string.Empty;
                int courseID = Parsers.ToInt(Request.Form["txtCourseID"].ToString() ?? string.Empty);

                // Find checked classes
                // The easy way to do this would be check all submitted form elements for anything that starts with "chkClass_"
                // The safe way to do this would be to load all valid classes, and compare to that list instead

                DistanceEdClassRepository classesRepo = new DistanceEdClassRepository(_config.GetConnectionString(FrontendSettings.ConnectionStringName));
                DistanceEdRequestRepository requestRepo = new LSSD.DistanceEdReg.Data.DistanceEdRequestRepository(_config.GetConnectionString(FrontendSettings.ConnectionStringName));
                Dictionary<int, DistanceEdClass> availableClasses = classesRepo.GetAvailableClasses(DateTime.Now).ToDictionary(x => x.ID);
                
                DistanceEdRequest newRequest = new DistanceEdRequest()
                {
                    StudentName = studentName,
                    StudentNumber = studentNumber,
                    StudentBaseSchool = studentSchool,
                    Comments = comments,
                    CourseID = courseID,
                    MentorTeacherName = mentorName,
                    Requestor = requestor
                };
                               
                
                // Validate
                
                if (courseID > 0) {                     
                    DistanceEdClass selectedClass = availableClasses[courseID] ?? null;
                    if (selectedClass == null)
                    {
                        regErrors.Add("Course with ID \"" + courseID + "\" was not found.");
                    }
                    else
                    {
                        if (selectedClass.MentorTeacherRequired)
                        {
                            if (string.IsNullOrEmpty(mentorName))
                            {
                                regErrors.Add("The class \"" + selectedClass.Name + "\" requires a mentor teacher, but no name was provided.");
                            }
                        }
                    }
                } else
                {
                    regErrors.Add("No course selected");
                }

                if (string.IsNullOrEmpty(studentName)) { regErrors.Add("Student name is required"); }
                if (string.IsNullOrEmpty(studentNumber)) { regErrors.Add("Student number is required"); }
                if (string.IsNullOrEmpty(studentSchool)) { regErrors.Add("Student base school is required"); }
                if (string.IsNullOrEmpty(requestor)) { regErrors.Add("Requestor name is required"); }


                // Submit to DB
                if (regErrors.Count == 0)
                {
                    try
                    {
                        requestRepo.AddNewRequest(newRequest);
                    }
                    catch (Exception ex)
                    {
                        regErrors.Add(ex.Message);
                    }
                }

                if (regErrors.Count == 0)
                {
                    return RedirectToPage("/Thanks");
                }
                else
                {
                    return Page();
                }
            } else
            {
                return Page();
            }
        }
    }
}
