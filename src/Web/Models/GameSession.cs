using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class GameSession
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public ICollection<UserGameSessions> Users { get; set; }
    }
}