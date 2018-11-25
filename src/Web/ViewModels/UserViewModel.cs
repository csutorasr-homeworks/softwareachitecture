using System;
using Web.Models;

namespace Web.Hubs
{
    public class UserViewModel
    {
        private UserGameSessions user;

        public UserViewModel(UserGameSessions user)
        {
            this.user = user;
            this.UserId = user.UserId;
            this.UserName = user.User.UserName;
        }

        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}