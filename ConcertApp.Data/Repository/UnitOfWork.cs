﻿using System;
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
        public IBookingRepository Bookings { get; private set; }
        public IConcertRepository Concerts { get; private set; }
        public IPerformanceRepository Performances { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
            Bookings = new BookingRepository(context);
            Users = new UserRepository(context);
            Concerts = new ConcertRepository(context);
            Performances = new PerfomanceRepository(context);

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
