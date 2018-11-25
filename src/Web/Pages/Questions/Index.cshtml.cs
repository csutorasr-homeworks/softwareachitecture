using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories;

namespace Web.Pages.Questions
{
    public class IndexModel : PageModel
    {
        private readonly IQuestionRepository repository;

        public IndexModel(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Question> Question { get; set; }

        public async Task OnGetAsync()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Question = await repository.GetQuestionForUser(new Guid(userId));
        }
    }
}
