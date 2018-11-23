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

        public async Task<GameSession> CreateGame(string code)
        {
            if (await dbContext.GameSessions.AnyAsync(x => x.Code == code))
            {
                throw new ArgumentException();
            }
            var game = new GameSession
            {
                Code = code,
            };
            await dbContext.GameSessions.AddAsync(game);
            await dbContext.SaveChangesAsync();
            return game;
        }

        public Task<GameSession> GetGame(Guid id)
        {
            return dbContext.GameSessions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<GameSession> GetGameByCode(string code)
        {
            return dbContext.GameSessions.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code);
        }
    }
}
