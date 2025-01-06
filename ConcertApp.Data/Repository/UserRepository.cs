using ConcertApp.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace ConcertApp.Data.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public ApplicationDbContext DbContext => Context as ApplicationDbContext;
        public UserRepository(ApplicationDbContext context)
            : base(context)
        {
        }
        public async Task<User> Find(string id)
        {
            return await DbContext.Users.FindAsync(id);
        }
        public async Task CreateAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await Context.Set<User>().AddAsync(user);

            await Context.SaveChangesAsync();
        }
    }
}
