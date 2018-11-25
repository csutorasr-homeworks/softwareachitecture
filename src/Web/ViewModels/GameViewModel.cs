using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Hubs
{
    internal class PlayerConnectedViewModel
    {

        public PlayerConnectedViewModel(GameSession game, Boolean Started)
        {
            this.GameId = game.Id;
            this.Users = new List<UserViewModel>();
            foreach(UserGameSessions user  in game.Users){
                Users.Add(new UserViewModel(user));
            }
            GameCanStart = game.Users.Count >= 2;
            GameStarted = Started;
        }

        public Guid GameId { get; set; }
        public Boolean GameCanStart { get; set; }
        public Boolean GameStarted { get; set; }

        public List<UserViewModel> Users { get; set; }
    }

    internal class GameCreationViewModel
    {

        public GameCreationViewModel(GameSession game)
        {
            GameId = game.Id;
            ErrorMessage = "";
            Success = true;
        }

        public Guid GameId { get; set; }
        public string ErrorMessage { get; set; }
        public Boolean Success { get; set; }
        
    }
}