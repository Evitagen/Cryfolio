using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryfolio.Services;
using Microsoft.EntityFrameworkCore;

namespace Cryfolio.Models
{
    public class Repo_CoinsHodle : DbContext, IDataStore<CoinsHodle>
    {
        public Repo_CoinsHodle(string dbPath)
        {
            _dbPath = dbPath;
            // Create database if not there. This will also ensure the data seeding will happen.
            Database.EnsureCreated();
        }

        private object _dbPath;

        public DbSet<CoinsHodle> CoinsHodles { get; set; }

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

         }

        public async Task<bool> AddItemAsync(CoinsHodle item)
        {
            //Debug.WriteLine("**** AddItemAsync");
            await CoinsHodles.AddAsync(item).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            try
            {
                var itemToRemove = CoinsHodles.FirstOrDefault(x => x.Id == id);
                if (itemToRemove != null)
                {
                    CoinsHodles.Remove(itemToRemove);
                    await SaveChangesAsync().ConfigureAwait(false);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<CoinsHodle> GetItemAsync(string id)
        {
            //Debug.WriteLine("**** GetItemAsync");
            var coinsHodle = await CoinsHodles.FirstOrDefaultAsync(x => x.Id.ToString() == id).ConfigureAwait(false);
            return coinsHodle;
        }

        public async Task<IEnumerable<CoinsHodle>> GetItemsAsync(bool forceRefresh = false)
        {
            //Debug.WriteLine("**** GetItemsAsync");
            // Ignore forceRefresh for now.
            var allItems = await CoinsHodles.ToListAsync().ConfigureAwait(false);
            return allItems;
        }

        public async Task<bool> UpdateItemAsync(CoinsHodle item)
        {
            try
            {
                CoinsHodles.Update(item);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
