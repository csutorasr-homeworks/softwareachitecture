using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Web.Models
{
    public class User : IdentityUser
    {
        public int Points { get; set; }
        public ICollection<UserGameSessions> Users { get; set; }
    }
}
