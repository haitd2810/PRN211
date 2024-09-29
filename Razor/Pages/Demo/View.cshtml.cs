using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Razor.Pages.Demo
{
    public class ViewModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost() {
            return Redirect("/Demo/View");
        }
    }
}
