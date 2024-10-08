using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoadDB_Razor.Models;

namespace LoadDB_Razor.Pages.StdNew
{
    public class CreateModel : PageModel
    {
        private readonly LoadDB_Razor.Models.PRN221Context _context;

        public CreateModel(LoadDB_Razor.Models.PRN221Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["DepartId"] = new SelectList(_context.Departments, "Id", "Name");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Students == null || Student == null)
            {
                return Page();
            }
            bool gender = Request.Form["gender"] == "true";
            Student.Gender = gender;
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
