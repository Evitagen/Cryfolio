using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryfolio.Services;
using Microsoft.EntityFrameworkCore;

namespace Cryfolio.Models
{
    public class Repo : DbContext, IDataStore<Portfolio>
    {

        /// <param name="dbPath">the platform specific path to the database</param>
        public Repo(string dbPath) : base()
        {
            _dbPath = dbPath;
            // Create database if not there. This will also ensure the data seeding will happen.
            Database.EnsureCreated();
        }

        private object _dbPath;

        public DbSet<CoinsHodle> CoinsHodles { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Make ID property the primary key.
            modelBuilder.Entity<CoinsHodle>()
                .HasKey(p => p.Id);

            // Coin name required
            modelBuilder.Entity<CoinsHodle>()
                .Property(p => p.Name)
                .IsRequired();


            // Transaction ID is Primary Key
            modelBuilder.Entity<Transactions>()
                .HasKey(p => p.Id);


            // Make ID property the primary key.
            modelBuilder.Entity<Portfolio>()
                .HasKey(p => p.PortfolioID);    

            // Portfolio Name Required
            modelBuilder.Entity<Portfolio>()
                .Property(p => p.PortfolioName)
                .IsRequired();
        }


        #region IDataStore<Portfolio> start
        public async Task<Portfolio> GetPortfiolioAsync(int id)
        {
            //Debug.WriteLine("**** GetItemAsync");
            var portfolio = await Portfolios.FirstOrDefaultAsync(x => x.PortfolioID == id).ConfigureAwait(false);
            return portfolio;
        }

        public async Task<IEnumerable<Portfolio>> GetPortfolioAsync(bool forceRefresh = false)
        {
            //Debug.WriteLine("**** GetItemsAsync");
            // Ignore forceRefresh for now.
            var allItems = await Portfolios.ToListAsync().ConfigureAwait(false);
            return allItems;
        }

        public async Task<bool> AddPortfolioAsync(Portfolio portfolio)
        {
            //Debug.WriteLine("**** AddItemAsync");
            await Portfolios.AddAsync(portfolio).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> UpdatePortfolioAsync(Portfolio portfolio)
        {
            //Debug.WriteLine("**** UpdateItemAsync");
            Portfolios.Update(portfolio);
            await SaveChangesAsync().ConfigureAwait(false);
            // No error handling. Homework :-)
            return true;
        }

        public async Task<bool> DeletePortfolioAsync(int id)
        {
            //Debug.WriteLine("**** DeleteItemAsync");
            var itemToRemove = Portfolios.FirstOrDefault(x => x.PortfolioID == id);
            if (itemToRemove != null)
            {
                Portfolios.Remove(itemToRemove);
                await SaveChangesAsync().ConfigureAwait(false);
            }

            return true;
        }

        public Task<bool> DeletePortfolioAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Portfolio> GetPortfolioAsync(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}   
