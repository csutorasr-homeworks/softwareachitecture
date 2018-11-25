using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories
{
    public interface IGameRepository
    {
        Task<GameSession> CreateGame(string code, int questionCount = 10, int maxUsers = 4);
        Task<GameSession> JoinGame(string gameId, string userId);
        Task<GameSession> GetGame(Guid id);
        Task<GameSession> GetGameByCode(string code);
        Task<List<GameSession>> GetAvailableGames();
        Task<GameSession> GetGameForUser(Guid userId, bool waiting, bool inprogress, bool ended);
        Task<GameSession> UpdateGame(GameSession game);
    }
}