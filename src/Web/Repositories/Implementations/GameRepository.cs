using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Data;
using Web.Models;

namespace Web.Repositories.Implementations
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext dbContext;

        public GameRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task<GameSession> CreateGame()
        {
            throw new NotImplementedException();
        }

        public Task<GameSession> GetGame(Guid id)
        {
            return dbContext.GameSessions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<GameSession> GetGameByCode(string code)
        {
            return dbContext.GameSessions.FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
