using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages
{
    [Authorize]
    public class PlayModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}