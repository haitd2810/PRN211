using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoadDB_Razor.Models;
using Microsoft.AspNetCore.SignalR;
using CRUD_SignalR.Hubs;

namespace CRUD_SignalR.Pages.Std
{
    public class DeleteModel : PageModel
    {
        private readonly LoadDB_Razor.Models.PRN221Context _context;
        private readonly IHubContext<HubServer> _hub;
        public DeleteModel(LoadDB_Razor.Models.PRN221Context context, IHubContext<HubServer> _hub)
        {
            _context = context;
            this._hub = _hub;
        }

        [BindProperty]
      public Student Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            else 
            {
                Student = student;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }
            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                Student = student;
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
                await _hub.Clients.All.SendAsync("LoadAll");
            }

            return RedirectToPage("./Index");
        }
    }
}
