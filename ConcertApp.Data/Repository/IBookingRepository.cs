using ConcertApp.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConcertApp.Data.Repository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        public Task<Booking> Find(string id);
        public Task UpdateAsync(Booking booking);
        public void Delete(Booking entity);
        public Task CreateAsync(Booking booking);
    }
}
