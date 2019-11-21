using System;
using System.Collections.Generic;
using System.Timers;
using Cryfolio.Services;
using Models.CoinMarketPortfolio;

namespace Cryfolio.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private static System.Timers.Timer aTimer;
        public string Message { get; set; } = "default";

        public MainViewModel()
        {
            UpdatePrices();

            aTimer = new System.Timers.Timer(120000);  // every 120 seconds 2 min 
            Timer();
        }

        public void UpdatePrices()
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


        private void Timer()
        {
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            UpdatePrices();
        }


    }
}
