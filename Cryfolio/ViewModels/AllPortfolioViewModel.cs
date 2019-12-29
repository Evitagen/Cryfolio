using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Cryfolio.Services;
using Models.CoinMarketPortfolio;

namespace Cryfolio.ViewModels
{
    public class AllPortfolioViewModel : ViewModelBase
    {

        private static System.Timers.Timer aTimer;


        public AllPortfolioViewModel()
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





        internal async Task ExecuteLoad_ALL_PortfolioCommand(int PortfolioID)
        {
            //portfolioID = PortfolioID;
            //string strtemp;

            //if (IsBusy)
            //    return;

            //IsBusy = true;

            //try
            //{
            //    CoinsHodles.Clear();
            //    CoinsHodlesViews.Clear();
            //    var portfolio = await _repo.GetPortfolioAsync(PortfolioID.ToString());
            //    foreach (var item in portfolio.coinsHodle)
            //    {
            //        CoinsHodles.Add(item);
            //    }

            //    foreach (var item in CoinsHodles)
            //    {
            //        var cv = new CoinsHodlesView();
            //        cv.Id = item.Id;
            //        cv.Name = item.Name;
            //        cv.Quantity = item.Quantity;
            //        cv.Total = RecalcTotal(item, getCoinPrice(item.Name));
            //        strtemp = item.Name.Replace("-", "");  // removes the dash as xamarin wont allow
            //        cv.imagelocation = strtemp + ".png";
            //        cv.Price = getCoinPrice(item.Name);
            //        CoinsHodlesViews.Add(cv);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex);
            //}
            //finally
            //{
            //    GetTotalPortfolioValue();
            //    IsBusy = false;
            //}

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
