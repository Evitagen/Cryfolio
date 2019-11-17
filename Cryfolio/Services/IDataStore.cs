using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cryfolio.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddPortfolioAsync(T item);
        Task<bool> UpdatePortfolioAsync(T item);
        Task<bool> DeletePortfolioAsync(int id);
        Task<T> GetPortfolioAsync(string id);
        Task<IEnumerable<T>> GetPortfoliosAsync(bool forceRefresh = false);
    }
}
