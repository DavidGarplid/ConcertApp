using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertApp.Data;

namespace ConcertApp.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;
        public IBookingRepository Booking { get; private set; }
        public IConcertRepository Concert { get; private set; }
        public IPerformanceRepository Performance { get; private set; }
        public IUserRepository User { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Booking = new BookingRepository(context);
        }
        public async Task<int> Complete()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
