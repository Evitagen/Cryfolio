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



        /// <summary>
        ///                                 Portfolio
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///

        #region IDataStore<Portfolio> start
        public async Task<Portfolio> GetPortfolioAsync(string id)
                        {
                            //Debug.WriteLine("**** GetItemAsync");
                            var portfolio = await Portfolios.FirstOrDefaultAsync(x => x.PortfolioID.ToString() == id).ConfigureAwait(false);
                            return portfolio;
                        }

                        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(bool forceRefresh = false)
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

                        public async Task<bool> DeletePortfolioAsync(int id)
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




        /// <summary>
        ///                                 CoinsHodles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///

        #region IDataStore<Portfolio> start
        public async Task<CoinsHodle> GetCoinsHodlesAsync(string id)
        {
            //Debug.WriteLine("**** GetItemAsync");
            var coinsHodle = await CoinsHodles.FirstOrDefaultAsync(x => x.Id.ToString() == id).ConfigureAwait(false);
            return coinsHodle;
        }

        public async Task<IEnumerable<CoinsHodle>> GetCoinHodlesAsync(bool forceRefresh = false)
        {
            //Debug.WriteLine("**** GetItemsAsync");
            // Ignore forceRefresh for now.
            var allItems = await CoinsHodles.ToListAsync().ConfigureAwait(false);
            return allItems;
        }

        public async Task<bool> AddCoinHodleAsync(CoinsHodle coinshodle)
        {
            //Debug.WriteLine("**** AddItemAsync");
            await CoinsHodles.AddAsync(coinshodle).ConfigureAwait(false);
            await SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<bool> UpdateCoinHodleAsync(CoinsHodle coinsHodle)
        {

            try
            {
                CoinsHodles.Update(coinsHodle);
                await SaveChangesAsync().ConfigureAwait(false);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
         
        }

        public async Task<bool> DeleteCoinHodleAsync(int id)
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
        #endregion

    }
}   
