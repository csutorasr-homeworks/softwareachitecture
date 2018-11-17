﻿using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
        private const string LOBBY_NAME = "lobby";
        private static readonly Dictionary<string, string> userGroups = new Dictionary<string, string>();
        private readonly IGameRepository gameRepository;

        public GameHub(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
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

        private async Task CreateGame()
        {
            await gameRepository.CreateGame();
        }
        private async Task<GameSession> GetGameByCode(string code)
        {
            return await gameRepository.GetGameByCode(code);
        }
    }
}
