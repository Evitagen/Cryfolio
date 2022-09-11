using System;
using Cryfolio.Models;
using Microsoft.EntityFrameworkCore;

namespace Cryfolio.Services
{
    public class DataContext : DbContext
    {
        private string _dbPath;
        public DataContext(string path)
        {
            _dbPath = path;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<CoinsHodle> CoinsHodles { get; set; }
        public DbSet<Transactions> Transactions { get; set; }


        public void init()
        {
            Database.EnsureCreated();   // Create database if not there. This will also ensure the data seeding will happen.
        }
    }
}
