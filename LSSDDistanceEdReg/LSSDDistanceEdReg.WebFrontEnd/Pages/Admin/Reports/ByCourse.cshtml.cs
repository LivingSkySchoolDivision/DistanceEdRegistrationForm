using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LSSD.DistanceEdReg.Util;

namespace LSSDDistanceEdReg.WebFrontEnd
{
    public class ByCourseModel : PageModel
    {
        public void OnGet()
        {

        }


        public IActionResult OnPostFormSubmit()
        {
            string courseID = Request.Form["drpCourse"].ToString();
            int parsedCourseID = courseID.ToInt();
            if (parsedCourseID > 0)
            {
                return Redirect("/Admin/Reports/ByCourse/" + parsedCourseID);
            }

            return RedirectToPage("/Admin/Reports/ByCourse");
        }

    }
}