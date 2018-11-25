using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories.Interfaces
{
    public interface IUserGameSessionRepository
    {
        Task<UserGameSessions> Add(Guid game, string user);
        Task<List<UserGameSessions>> GetAllForGame(Guid game);
    }
}
