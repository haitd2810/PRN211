using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LoadDB_Razor.Models;
using CRUD_SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace CRUD_SignalR.Pages.Std
{
    public class CreateModel : PageModel
    {
        private readonly LoadDB_Razor.Models.PRN221Context _context;
        private readonly IHubContext<HubServer> _hub;
        public CreateModel(LoadDB_Razor.Models.PRN221Context context, IHubContext<HubServer> _hub)
        {
            _context = context;
            this._hub = _hub;
        }

        public IActionResult OnGet()
        {
        ViewData["DepartId"] = new SelectList(_context.Departments, "Id", "Id");
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

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("LoadAll");

            return RedirectToPage("./Index");
        }
    }
}
