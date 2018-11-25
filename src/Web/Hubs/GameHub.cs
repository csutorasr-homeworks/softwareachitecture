using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Repositories;
using Web.Repositories.Interfaces;
using Web.ViewModels;

namespace Web.Hubs
{
    [Authorize]
    public class GameHub : Hub
    {
        private const string LOBBY_NAME = "lobby";
        private static readonly Dictionary<string, string> userGroups = new Dictionary<string, string>();
        private readonly IGameRepository gameRepository;
        private readonly IUserGameSessionRepository userGameSessionRepository;

        public GameHub(IGameRepository gameRepository, IUserGameSessionRepository userGameSessionRepository)
        {
            this.gameRepository = gameRepository;
            this.userGameSessionRepository = userGameSessionRepository;
        }

        public string GetUserName()
        {
            return Context.User.Identity.Name ?? "Not registered user";
        }

        public async override Task OnConnectedAsync()
        {
            await AddToLobby();
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string message)
        {
            await CurrentGroupUsers().SendAsync("ReceiveMessage", GetUserName(), message);
        }

        private async Task AddToLobby()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, LOBBY_NAME);
            userGroups.Add(Context.ConnectionId, LOBBY_NAME);
        }

        private IClientProxy CurrentGroupUsers()
        {
            return Clients.Group(userGroups[Context.ConnectionId]);
        }
        
        public async Task CreateGame(string code,int nrOfPlayers,int nrOfQuestions)
        {
            var userId = Context.UserIdentifier;
            var game = await gameRepository.CreateGame(System.Guid.NewGuid().ToString());
            await JoinGame(game.Id.ToString());
            await GetGames();
        }

        public async Task JoinGame(string gameId)
        {
            var userId = Context.UserIdentifier;
            var gameIdGuid = new System.Guid(gameId);
            var currentPlayers = await userGameSessionRepository.GetAllForGame(gameIdGuid);
            var game = await gameRepository.GetGame(gameIdGuid);
            //TODO check for game state waiting
            if (game.WaitingForPlayers)
            {
                if (!currentPlayers.Exists(x => x.UserId == userId))
                {
                    await userGameSessionRepository.Add(gameIdGuid, userId);
                    game = await gameRepository.GetGame(gameIdGuid);
                }
                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                await Clients.Group(game.Id.ToString()).SendAsync("PlayerConnected", new PlayerConnectedViewModel(game));
            }
            else
            {
                //TODO check for maxusers     
            }

        }

        public async Task GetGames()
        {
            await Clients.All.SendAsync("GameListUpdate", new GameListViewModel(await gameRepository.GetAvailableGames()));
        }

        public async Task StartGame()
        {
            var userId = Context.UserIdentifier;
            var gameSession = await gameRepository.GetGameForUser(userId, true, false, false);
            if (gameSession != null)
            {
                gameSession.WaitingForPlayers = false;
                gameSession.InProgress = true;
                gameSession = await gameRepository.UpdateGame(gameSession);
                await Clients.Group(gameSession.Id.ToString()).SendAsync("PlayerConnected", new PlayerConnectedViewModel(gameSession));
            }
            else
            {
                //todo handle if already started
            }

        }

        public async Task<bool> SendGuess(string answerId)
        {
            var userId = Context.UserIdentifier;
            var gameSession = await gameRepository.GetGameForUser(userId, false, true, false);
            if (gameSession != null)
            {
                await gameRepository.SelectAnswer(gameSession.Id, userId, new System.Guid(answerId));
                return true;
            }
            else
            {
                return false;
            }
        }

        //public async Task SendScores(string GameId)
        //{
        //    await Clients.Group(GameId).SendAsync("PlayerConnected", new ScoreVievModel());
        //}
    }
}
