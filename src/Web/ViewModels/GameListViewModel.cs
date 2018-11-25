using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Hubs
{
    internal class GameListViewModel
    {
        private object p;
        public List<GameViewModel> games = new List<GameViewModel>();


        public GameListViewModel(List<GameSession> list)
        {
            foreach(GameSession game in list)
            {

                this.games.Add(new GameViewModel
                {
                    GameId = game.Id
                });
            }
        }
    }

    internal class GameViewModel
    {

        public Guid GameId { get; set; }
    }
}