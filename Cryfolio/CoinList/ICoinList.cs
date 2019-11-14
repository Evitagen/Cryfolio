using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoinMarketPortfolio;

namespace Cryfolio._repo
{
    public interface ICoinList
    {
       Task<List<Coins>> GetCoinPrices();
    }
}
