using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Web.Pages
{
    [Authorize]
    public class PlayModel : PageModel
    {
        public string UserId { get; private set; }

        public void OnGet()
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}