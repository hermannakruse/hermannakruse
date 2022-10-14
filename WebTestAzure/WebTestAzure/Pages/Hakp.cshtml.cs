using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebTestAzure.Pages
{
    public class HakpModel : PageModel
    {
        public void OnGet()
        {
            Program.showSec = true;
        }
    }
}
