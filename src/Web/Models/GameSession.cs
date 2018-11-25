using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class GameSession
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public bool WaitingForPlayers { get; set; }
        public bool InProgress { get; set; }
        public bool Finnished { get; set; }
        public int MaxUsers { get; set; }
        public ICollection<UserGameSessions> Users { get; set; }
    }
}