using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories;

namespace Web.Pages.Questions
{
    public class DetailsModel : PageModel
    {
        private readonly IQuestionRepository repository;

        public DetailsModel(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public Question Question { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Question = await repository.GetQuestion(id.Value);

            if (Question == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
