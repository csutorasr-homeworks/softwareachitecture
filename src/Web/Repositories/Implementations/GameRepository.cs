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
                QuestionCount = questionCount 
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
                UserId = userId
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
            var games = await dbContext.GameSessions.AsNoTracking().Where(x => x.WaitingForPlayers).Take(5).ToListAsync();
            return games;
        }

        public async Task<GameSession> GetGameForUser(string userId, bool waiting, bool inprogress, bool ended)
        {
            var game = await dbContext.GameSessions.Include(x => x.Users).ThenInclude(x => x.User).AsNoTracking().Where(x => x.WaitingForPlayers == waiting && x.InProgress == inprogress && x.Finnished == ended).FirstOrDefaultAsync(x => x.Users.Any(y => y.UserId == userId) );   
            return game;
        }
        
        public async Task<GameSession> UpdateGame(GameSession game)
        {
            dbContext.Update(game);
            await dbContext.SaveChangesAsync();
            return game;
        }
        public async Task<GameQuestion> GetQuestion(Guid gameId)
        {
            var currentQuestion = await dbContext.GameSessions.AsNoTracking().Where(x => x.Id == gameId).Select(x => x.CurrentQuestion).FirstOrDefaultAsync();
            return await dbContext.GameQuestions.AsNoTracking()
                .Where(x => x.GameId == gameId)
                .Include(x => x.Question)
                .ThenInclude(x => x.Answers)
                .Include(x => x.UserSelectedAnswers)
                .ThenInclude(x => x.UserGameSession)
                .Skip(currentQuestion)
                .FirstOrDefaultAsync();
        }
        public async Task SelectAnswer(Guid gameId, string userId, Guid answerId)
        {
            var answerTime = DateTime.Now;
            var answer = await dbContext.Answers.Include(x => x.Question).AsNoTracking().FirstOrDefaultAsync(x => x.Id == answerId);
            if (answer == null)
            {
                throw new ArgumentException();
            }
            var gameQuestion = await dbContext.GameQuestions.AsNoTracking()
                .Where(x => x.QuestionId == answer.QuestionId)
                .Where(x => x.GameId == gameId)
                .Where(x => x.Game.Users.Any(u => u.UserId == userId))
                .FirstOrDefaultAsync();
            if (gameQuestion == null)
            {
                throw new ArgumentException();
            }
            var userGameSession = await dbContext.UserGameSessions.AsNoTracking().FirstOrDefaultAsync(x => x.GameSessionId == gameQuestion.GameId && x.UserId == userId);

            if (userGameSession == null)
            {
                throw new ArgumentException();
            }
            var hasAnswered = await dbContext.UserSelectedAnswers.AsNoTracking().AnyAsync(x => x.GameQuestionId == gameQuestion.Id && x.UserGameSessionId == userGameSession.Id);
            if (!hasAnswered)
            {
                var selectedAnswer = new UserSelectedAnswer
                {
                    UserGameSessionId = userGameSession.Id,
                    AnswerId = answerId,
                    AnswerTime = answerTime,
                    GameQuestionId = gameQuestion.Id,
                };
                await dbContext.UserSelectedAnswers.AddAsync(selectedAnswer);
                await dbContext.SaveChangesAsync();
            }
        }

        public Task<GameSession> GetResults(Guid gameId)
        {
            return dbContext.GameSessions
                .Where(x => x.Id == gameId)
                .Include(x => x.Users)
                .ThenInclude(x => x.User)
                .Include(x => x.Questions)
                .ThenInclude(x => x.Question)
                .Include(x => x.Questions)
                .ThenInclude(x => x.UserSelectedAnswers)
                .ThenInclude(x => x.Answer)
                .Include(x => x.Questions)
                .ThenInclude(x => x.UserSelectedAnswers)
                .ThenInclude(x => x.UserGameSession)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<GameSession>> GetEndedGames(string userId)
        {
            return await dbContext.GameSessions.Where(x => x.Finnished == true && x.Users.Select(u => u.UserId).Contains(userId)).ToListAsync();
        }
    }
}
