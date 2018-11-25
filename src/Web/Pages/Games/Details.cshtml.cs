using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories;
using Web.ViewModels;

namespace Web.Pages.Games
{
    public class DetailsModel : PageModel
    {
        private readonly IGameRepository repository;

        public DetailsModel(IGameRepository repository)
        {
            this.repository = repository;
        }

        public ResultViewModel GameResult { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GameSession = await repository.GetGame(id.Value);

            if (GameSession == null)
            {
                return NotFound();
            }
            GameResult = new ResultViewModel(GameSession);
            return Page();
        }
    }
}
