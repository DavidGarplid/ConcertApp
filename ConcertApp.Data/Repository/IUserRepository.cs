using ConcertApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task CreateAsync(User user);
        Task<User?> Find(string id);
    }
}
