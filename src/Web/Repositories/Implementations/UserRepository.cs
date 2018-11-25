using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Models;
using Web.Repositories.Interfaces;

namespace Web.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public Task<User> GetUser(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
