using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.ViewModels
{
    internal class GameListViewModel
    {
        public List<GameViewModel> games = new List<GameViewModel>();


        public GameListViewModel(List<GameSession> list)
        {
            foreach (GameSession game in list)
            {
                games.Add(new GameViewModel
                {
                    Code = game.Code,
                    GameId = game.Id,
                    MaxUsers = game.MaxUsers
                });
            }
        }
    }

    internal class GameViewModel
    {
        public string Code { get; set; }
        public Guid GameId { get; set; }
        public int MaxUsers { get; set; }
        public Guid QuestionNr { get; set; }
    }
}