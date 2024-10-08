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

        public IndexModel(LoadDB_Razor.Models.PRN221Context context)
        {
            _context = context;
        }

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Students != null)
            {
                Student = await _context.Students
                .Include(s => s.Depart).ToListAsync();
            }
        }
    }
}
