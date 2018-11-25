using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels
{
    internal class PlayerConnectedViewModel
    {

        public PlayerConnectedViewModel(GameSession game)
        {
            this.GameId = game.Id;
            this.Users = new List<UserViewModel>();
            foreach(UserGameSessions user  in game.Users){
                Users.Add(new UserViewModel(user));
            }
            GameCanStart = true;
            GameStarted = game.InProgress == true;
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