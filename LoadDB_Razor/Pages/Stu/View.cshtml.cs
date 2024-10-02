using LoadDB_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoadDB_Razor.Pages.Stu
{
    public class ViewModel : PageModel
    {
        public List<Student> Stds = new List<Student>();
        private readonly PRN221Context con;
        public ViewModel(PRN221Context _con)
        {
            con = _con;
        }
        public void OnGet()
        {
            Stds = con.Students.Include(x => x.Depart).ToList();
        }
    }
}
