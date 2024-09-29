using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Demo
{
    public class IndexModel : PageModel
    {
        [BindProperty] //cach 4 them binding property
        public int Id { get; set; } = 0;
        [BindProperty]  //cach 4 them binding property
        public string Name { get; set; } = "Nguyen Van A";

        public void OnGet()
        {
        }

        //cach dung giong MVC (Xai ViewData de truyen du lieu)
        //public IActionResult OnGet()
        //{
        //    ViewData["Name"] = name;
        //    return Page();
        //}

        //cack 1:
        //public IActionResult OnPost()
        //{
        //    Name = Request.Form["name"];
        //    Id = int.Parse(Request.Form["id"]);
        //    return Page();
        //}

        //cach 2:
        //public IActionResult OnPost(IFormCollection format)
        //{
        //    Name = format["name"];
        //    Id = int.Parse(format["id"]);
        //    return Page();
        //}

        //cack 3:
        //public IActionResult OnPost(string id, string name)
        //{
        //    Name = name;
        //    Id = int.Parse(id);
        //    return Page();
        //}

        //cach 4:
        
        public IActionResult OnPost()
        {

            return Page();
        }
    }
}
