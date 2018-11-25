using System;

namespace Web.Models
{
    public class UserGameSessions
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid GameSessionId { get; set; }
        public GameSession GameSession { get; set; }
        public int Points { get; set; }
    }
}