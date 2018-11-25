using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;
using Web.Repositories;

namespace Web.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly IGameRepository repository;

        public IndexModel(IGameRepository repository)
        {
            this.repository = repository;
        }

        public IList<GameSession> GameSession { get;set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            GameSession = await repository.GetEndedGames(userId);
        }
    }
}
