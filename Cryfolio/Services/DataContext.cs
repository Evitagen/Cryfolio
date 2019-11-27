using System;
using Cryfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Cryfolio.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();   // Create database if not there. This will also ensure the data seeding will happen.
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<CoinsHodle> CoinsHodles { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

    }
}
