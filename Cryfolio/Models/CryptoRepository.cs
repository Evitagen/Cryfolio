using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Cryfolio.Services;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Cryfolio.Models
{
    public class CryptoRepository : ICryptoRepository
    {
        private readonly DataContext _context;

        public CryptoRepository(DataContext context)
        {
            _context = context;
            _context.init();
        }



        /// 
        ///  portfolio
        /// 
        /// 
        /// 
        ///

        public async Task<bool> AddPortfolioAsync(Portfolio portfolio)
        {
            Debug.WriteLine("**** AddItemAsync");
            _context.Portfolios.Add(portfolio);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePortfolioAsync(Portfolio portfolio)
        {
            try
            {
                _context.Portfolios.Update(portfolio);
                await _context.SaveChangesAsync().ConfigureAwait(false);
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

        public async Task<Portfolio> GetPortfolioAsync(string id)
        {
            Debug.WriteLine("**** GetPortfolioAsync");

            try
            {
                var portfolio = await _context.Portfolios
                  .Include(c => c.coinsHodle)
                  .FirstOrDefaultAsync(p => p.PortfolioID.ToString() == id);

                return portfolio;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
          
        }

        public async Task<IEnumerable<Portfolio>> GetPortfoliosAsync(bool forceRefresh = false)
        {
            try
            {
                var allItems = await _context.Portfolios.ToListAsync().ConfigureAwait(false);
                List<Portfolio> portfolios = new List<Portfolio>();

                foreach (var portfolio in allItems)
                {
                    portfolios.Add(await GetPortfolioAsync(portfolio.PortfolioID.ToString()));
                }

                return portfolios;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
     
        }




        /// 
        /// coinshodle          
        /// 
        /// 
        /// 
        ///

        public async Task<bool> AddCoinsHodleAsync(CoinsHodle coinshodle)
        {
            Debug.WriteLine("**** AddItemAsync");
            await _context.CoinsHodles.AddAsync(coinshodle).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public Task<bool> UpdateCoinsHodleAsync(CoinsHodle item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCoinsHodleAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CoinsHodle> GetCoinsHodleAsync(string id)
        {
            Debug.WriteLine("**** GetCoinsHodleAsync");
            var coinsHodle = await _context.CoinsHodles.FirstOrDefaultAsync(x => x.Id.ToString() == id).ConfigureAwait(false);
            return coinsHodle;
        }

        public async Task<IEnumerable<CoinsHodle>> GetCoinsHodlesAsync(bool forceRefresh = false)
        {
            Debug.WriteLine("**** GetCoinsHodlessAsync");
            // Ignore forceRefresh for now.
            var coinsHodles = await _context.CoinsHodles.ToListAsync().ConfigureAwait(false);
            return coinsHodles;             
        }





        ///
        /// Transactions
        ///
        ///

        public async Task<bool> AddTransactionAsync(Transactions transactions)
        {
            Debug.WriteLine("**** AddTransactionAsync");
            await _context.Transactions.AddAsync(transactions).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public Task<bool> UpdateTransactionAsync(CoinsHodle item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteTransactionAsync(int id)
        {
            try
            {
                var TransactionToRemove = _context.Transactions.FirstOrDefault(x => x.Id == id);
                if (TransactionToRemove != null)
                {
                    _context.Transactions.Remove(TransactionToRemove);
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

        public async Task<Transactions> GetTransactionAsync(string id)
        {
            Debug.WriteLine("**** GetTransactionAsync");
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id.ToString() == id).ConfigureAwait(false);
            return transaction;
        }

        public async Task<IEnumerable<Transactions>> GetTransactionsAsync(bool forceRefresh = false)
        {
            Debug.WriteLine("**** GetTransactionsAsync");
            // Ignore forceRefresh for now.
            var transactions = await _context.Transactions.ToListAsync().ConfigureAwait(false);
            return transactions;
        }


    }
}
