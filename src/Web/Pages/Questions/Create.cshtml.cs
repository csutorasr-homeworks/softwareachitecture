using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Repositories;

namespace Web.Pages.Questions
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IQuestionRepository repository;

        public CreateModel(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public QuestionVM Question { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await repository.AddQuestion(new System.Guid(userId), Question.Text, Question.CorrectAnswer, new List<string>
            {
                Question.IncorrectAnswer1,
                Question.IncorrectAnswer2,
                Question.IncorrectAnswer3,
            });

            return RedirectToPage("./Index");
        }

        public class QuestionVM
        {
            public string Text { get; set; }
            public string CorrectAnswer { get; set; }
            public string IncorrectAnswer1 { get; set; }
            public string IncorrectAnswer2 { get; set; }
            public string IncorrectAnswer3 { get; set; }
        }
    }
}