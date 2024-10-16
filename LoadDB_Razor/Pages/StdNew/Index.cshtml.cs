using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoadDB_Razor.Models;

namespace LoadDB_Razor.Pages.StdNew
{
    public class IndexModel : PageModel
    {
        private readonly LoadDB_Razor.Models.PRN221Context _context;
        [BindProperty]
        public int state { get; set; } = 0;
        bool firstLoad = true;
        public IndexModel(LoadDB_Razor.Models.PRN221Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Students != null && firstLoad)
            {
                Student = await _context.Students
                .Include(s => s.Depart).ToListAsync();
                firstLoad = false;
            }
        }

        private void xuly()
        {
            state %= 4;
            dynamic st = null;
            if (state == 1)
            {
                Student = _context.Students
                    .Include(s => s.Depart).OrderBy(x => x.Name).ToList();
            }
            else if (state == 2)
            {
                Student = _context.Students
                    .Include(s => s.Depart).OrderByDescending(x => x.Name).ToList();
            }
            else if (state == 3)
            {
                Student = _context.Students
                    .Include(s => s.Depart).OrderByDescending(x => x.Id).ToList();
            }
            else
            {
                Student = _context.Students
                .Include(s => s.Depart).ToList();
            }
        }
        public IActionResult OnPost()
        {
            state = int.Parse(Request.Form["state"]);
            xuly();
            return Page();
        }
    }
    //sử dụng filter theo combo box hoặc radio box
}
