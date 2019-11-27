using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cryfolio.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cryfolio.Models
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly DataContext _context;
        private object _dbPath;

        public CryptoRepository(string dbPath)
        {
            _dbPath = dbPath;
            //_context.Database.EnsureCreated();   // Create database if not there. This will also ensure the data seeding will happen.
        }









        public async Task<bool> AddPortfolioAsync(Portfolio portfolio)
        {
            Debug.WriteLine("**** AddItemAsync");
            _context.Portfolios.Add(portfolio);
            return await _context.SaveChangesAsync() > 0;
        }

        public Task<bool> UpdatePortfolioAsync(Portfolio item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeletePortfolioAsync(int id)
        {
            try
            {
                var itemToRemove = _context.Portfolios.FirstOrDefault(x => x.PortfolioID == id);
                if (itemToRemove != null)
                {
                    _context.Portfolios.Remove(itemToRemove);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public Task<Portfolio> GetPortfolioAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Portfolio>> GetPortfoliosAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }





        public Task<bool> AddCoinsHodleAsync(CoinsHodle item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCoinsHodleAsync(CoinsHodle item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCoinsHodleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CoinsHodle> GetCoinsHodleAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CoinsHodle>> GetCoinsHodlesAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }



    }
}
