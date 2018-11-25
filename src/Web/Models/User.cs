using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Web.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserGameSessions> Games { get; set; }
    }
}
