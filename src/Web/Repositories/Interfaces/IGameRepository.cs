using System;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Repositories
{
    public interface IGameRepository
    {
        Task<GameSession> CreateGame(string code);
        Task<GameSession> GetGame(Guid id);
        Task<GameSession> GetGameByCode(string code);
    }
}