using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cryfolio.Services;
using Microsoft.EntityFrameworkCore;

namespace Cryfolio.Models
{
    public class Repo_CoinsHodle : DbContext, IDataStore<CoinsHodle>
    {
        public Repo_CoinsHodle()
        {
        }

        public Task<bool> AddPortfolioAsync(CoinsHodle item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePortfolioAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CoinsHodle> GetPortfolioAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CoinsHodle>> GetPortfoliosAsync(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatePortfolioAsync(CoinsHodle item)
        {
            throw new NotImplementedException();
        }
    }
}
