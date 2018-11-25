using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Models
{
    public class GameSession
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool WaitingForPlayers { get; set; }
        public bool InProgress { get; set; }
        public int CurrentQuestion { get; set; }
        public bool Finnished { get; set; }
        public int MaxUsers { get; set; }
        public ICollection<UserGameSessions> Users { get; set; }
        public ICollection<GameQuestion> Questions { get; internal set; }
    }
}