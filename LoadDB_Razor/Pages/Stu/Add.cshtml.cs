using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LoadDB_Razor.Pages.Stu
{
    public class AddModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            return Redirect("View");
        }
    }
}
//Task:
//thử CRUD truyền data bằng cả 4 cách
// Gender checkbox - radio box - combobox
// Department checkbox - radio box - combobox
// Có tất cả 36 dạng bài, nhưng làm ít nhất 9 dạng bài (tự chọn) (ít nhất mỗi phương pháp làm 2 bài)
