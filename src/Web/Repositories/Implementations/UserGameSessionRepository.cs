using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class UserGameSessionRepository : IUserGameSessionRepository
    {
        private readonly ApplicationDbContext dbContext;


        public UserGameSessionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserGameSessions> Add(Guid gameId, Guid userId)
        {
            var userGameSession = new UserGameSessions
            {
                GameSessionId = gameId,
                UserId = userId
            };
            await dbContext.UserGameSessions.AddAsync(userGameSession);
            await dbContext.SaveChangesAsync();
            return userGameSession;
        }

        public Task<List<UserGameSessions>> GetAllForGame(Guid game)
        {
            return dbContext.UserGameSessions.AsNoTracking().Where(x => x.GameSessionId == game).ToListAsync();
        }
    }
}
