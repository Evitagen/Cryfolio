using System;
using System.Collections.Generic;
using Cryfolio.Services;
using Models.CoinMarketPortfolio;

namespace Cryfolio.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
     
        public MainViewModel()
        {
            var services = new CoinList();
            CoinmarketCap_Coins = services.GetCoinPrices().Result;
        }
        private List<Coins> coinmarketCap_Coins;

        public List<Coins> CoinmarketCap_Coins
        {
            get { return coinmarketCap_Coins; }
            set { SetProperty(ref coinmarketCap_Coins, value); }
        }

       
    }
}
