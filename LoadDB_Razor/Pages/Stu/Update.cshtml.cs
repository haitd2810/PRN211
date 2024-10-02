using LoadDB_Razor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace LoadDB_Razor.Pages.Stu
{
    public class UpdateModel : PageModel
    {
        public int Id { get; set; }
        [BindProperty]
        public Student std {  get; set; }
        [BindProperty]
        public List<Department> DList { get; set; }
        public void OnGet(string sID)
        {
            try
            {
                Id = int.Parse(sID);
                std = PRN221Context.Ins.Students.Include(x => x.Depart).FirstOrDefault(x => x.Id == Id);
                DList = PRN221Context.Ins.Departments.ToList();
            }catch(Exception ex)
            {
                Id = -1;
            }
        }
        public IActionResult OnPost()
        {
            if (std.Gpa >= 0 && std.Gpa <= 10 && IsValidDate(DateTime.Parse(std.Dob.ToString())))
            {
                Student st = PRN221Context.Ins.Students.Include(x => x.Depart).FirstOrDefault(st => st.Id == std.Id);
                if (st != null)
                {
                    st.Name = std.Name;
                    st.Gender = std.Gender;
                    st.DepartId = std.DepartId;
                    st.Gpa = std.Gpa;
                    st.Dob = std.Dob;
                    PRN221Context.Ins.Students.Update(st);
                    PRN221Context.Ins.SaveChanges();
                }
            }

            return Redirect("View");
        }
        private bool IsValidDate(DateTime date)
        {
            if (date.CompareTo(DateTime.Now) <= 0)
            {
                return true;
            }
            return false;
        }
    }
}

//tuong tu tao gender = checkbox, combobox, radio
// department = checkbox, combobox radio,
//voi moi cach se chon 1 trong 4 phuong phap gui du lieu
