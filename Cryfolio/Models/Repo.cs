using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cryfolio.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

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

        //public DbSet<CoinsHodle> CoinsHodles { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
       

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



            // Add some initial data when runing in debug
        #if DEBUG




            modelBuilder.Entity<Portfolio>()
                .HasData(
                    new Portfolio { PortfolioID = 1, PortfolioName = "Main" },
                    new Portfolio { PortfolioID = 2, PortfolioName = "Mobile" },
                    new Portfolio { PortfolioID = 3, PortfolioName = "Other" }
                );
        #endif

        }


        #region IDataStore<Portfolio> start
                        public async Task<Portfolio> GetItemAsync(string id)
                        {
                            //Debug.WriteLine("**** GetItemAsync");
                            var portfolio = await Portfolios.FirstOrDefaultAsync(x => x.PortfolioID.ToString() == id).ConfigureAwait(false);
                            return portfolio;
                        }

                        public async Task<IEnumerable<Portfolio>> GetItemsAsync(bool forceRefresh = false)
                        {
                            //Debug.WriteLine("**** GetItemsAsync");
                            // Ignore forceRefresh for now.
                            var allItems = await Portfolios.ToListAsync().ConfigureAwait(false);
                            return allItems;
                        }
         
                        public async Task<bool> AddItemAsync(Portfolio portfolio)
                        {
                            //Debug.WriteLine("**** AddItemAsync");
                            await Portfolios.AddAsync(portfolio).ConfigureAwait(false);
                            await SaveChangesAsync().ConfigureAwait(false);
                            return true;
                        }

                        public async Task<bool> UpdateItemAsync(Portfolio portfolio)
                        {
                           try
                            {
                                Portfolios.Update(portfolio);
                                await SaveChangesAsync().ConfigureAwait(false);
                                return true;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                                return false;
                            }
                        }

                        public async Task<bool> DeleteItemAsync(int id)
                        {
                            try
                            {
                                var itemToRemove = Portfolios.FirstOrDefault(x => x.PortfolioID == id);
                                if (itemToRemove != null)
                                {
                                    Portfolios.Remove(itemToRemove);
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
        #endregion


    }
}   
