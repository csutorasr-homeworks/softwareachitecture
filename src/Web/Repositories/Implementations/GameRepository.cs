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

        public async Task<GameSession> CreateGame(string code, int questionCount, int maxUsers)
        {
            if (await dbContext.GameSessions.AnyAsync(x => x.Code == code))
            {
                throw new ArgumentException();
            }
            var game = new GameSession
            {
                Code = code,
                Finnished = false,
                InProgress = false,
                WaitingForPlayers = true,
                Questions = await dbContext.Questions.OrderBy(x => Guid.NewGuid()).Take(questionCount).Select(x => new GameQuestion
                {
                    QuestionId = x.Id,
                }).ToListAsync(),
                MaxUsers = maxUsers,
            };

            await dbContext.GameSessions.AddAsync(game);
            await dbContext.SaveChangesAsync();
            return game;
        }

        public async Task<GameSession> JoinGame(string gameId, string userId)
        {
            var game = await GetGame(new Guid(gameId));
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);

            var userGameSession = new UserGameSessions
            {
                GameSessionId = new Guid(gameId),
                UserId = new Guid(userId)
            };
            await dbContext.UserGameSessions.AddAsync(userGameSession);
            await dbContext.SaveChangesAsync();
            return game;
        }

        public async Task<GameSession> GetGame(Guid id)
        {
            var game = await dbContext.GameSessions.Include(x => x.Users).ThenInclude(x => x.User).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return game;
        }

        public Task<GameSession> GetGameByCode(string code)
        {
            return dbContext.GameSessions.AsNoTracking().FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<List<GameSession>> GetAvailableGames()
        {
            var games = await dbContext.GameSessions.AsNoTracking().Where(x => x.WaitingForPlayers).Take(10).ToListAsync();
            return games;
        }

        public async Task<GameSession> GetGameForUser(Guid userId, bool waiting, bool inprogress, bool ended)
        {
            var game = await dbContext.GameSessions.AsNoTracking().Where(x => x.WaitingForPlayers == waiting && x.InProgress == inprogress && x.Finnished == ended).FirstOrDefaultAsync(x => x.Users.Any(y => y.UserId == userId) );
            return game;
        }
        
        public async Task<GameSession> UpdateGame(GameSession game)
        {
            dbContext.Attach(game);
            await dbContext.SaveChangesAsync();
            return game;
        }
    }
}
