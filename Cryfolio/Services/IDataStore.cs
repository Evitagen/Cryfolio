using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cryfolio.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddPortfolioAsync(T item);
        Task<bool> UpdatePortfolioAsync(T item);
        Task<bool> DeletePortfolioAsync(string id);
        Task<T> GetPortfolioAsync(int id);
        Task<IEnumerable<T>> GetPortfolioAsync(bool forceRefresh = false);
    }
}
