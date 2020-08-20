using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSSD.DistanceEdReg;
using LSSD.DistanceEdReg.Util;
using LSSDDistanceEdReg.WebFrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LSSDDistanceEdReg.WebFrontEnd.Pages.Admin
{
    public class EditCourseModel : PageModel
    {
        private DistanceEdClassService _classService;

        public DistanceEdClass SelectedClass { get; private set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public EditCourseModel(DistanceEdClassService DistanceEdClassService)
        {
            _classService = DistanceEdClassService;
        }

        public IActionResult OnPost()
        {
            Console.WriteLine("POST");
                        
            // Load the object in its pre-edited state (in case we miss a field or something)
            DistanceEdClass dec = _classService.Get(Id);

            // Note: If there's a way to do this as nice as Blazor handles this, I'd prefer to do it that way.
            if (dec != null)
            {
                // Parse the values from the form
                string name = Request.Form["SelectedClass.Name"].ToString().Trim();
                string description = Request.Form["SelectedClass.Name"].ToString().Trim();
                string moreinfourl = Request.Form["SelectedClass.Name"].ToString().Trim();
                string deliverymethod = Request.Form["SelectedClass.Name"].ToString().Trim();
                string prereqs = Request.Form["SelectedClass.Name"].ToString().Trim();
                string blackboardid = Request.Form["SelectedClass.Name"].ToString().Trim();


                // Make changes from the form


                // Apply the changes
            }

            // Redirect back
            return RedirectToPage("/admin/courses");
        }


        public void OnGet()
        {
            if (Id != 0)
            {
                SelectedClass = _classService.Get(Id);
            }            
        }
    }
}