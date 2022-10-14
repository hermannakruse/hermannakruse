using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebTestAzure.Pages
{
    public class HakpModel : PageModel
    {
        public void OnGet()
        {
            var id = HttpContext.Connection.Id;
            Program.showSec.Add(id);
        }
    }
}
