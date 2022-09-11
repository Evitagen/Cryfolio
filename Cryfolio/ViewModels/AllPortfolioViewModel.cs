using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Cryfolio.Models;
using Cryfolio.Services;
using Models.CoinMarketPortfolio;
using Xamarin.Forms;

namespace Cryfolio.ViewModels
{
    public class AllPortfolioViewModel : ViewModelBase
    {
        public List<Portfolio> Portfolios { get; set; }
        public List<AllCoinsHodle> AllCoinsHodles { get; set; }
        public ObservableCollection<AllCoinsHodleView> AllCoinsHodlesViews { get; set; }
        public decimal Total { get; set; }
        private readonly ICryptoRepository _repo;

        private static System.Timers.Timer aTimer;


        public Command LoadItemsCommand { get; set; }


        public AllPortfolioViewModel()
        {
            Portfolios = new List<Portfolio>();
            AllCoinsHodles = new List<AllCoinsHodle>();
            AllCoinsHodlesViews = new ObservableCollection<AllCoinsHodleView>();


            UpdatePrices();
            aTimer = new System.Timers.Timer(120000);  // every 120 seconds 2 min 
            Timer();

            _repo = CryptoRepository;
            LoadItemsCommand = new Command(async () => await ExecuteLoadPortfoliosCommand());
        }

        public void UpdatePrices()
        {
            var services = new CoinList();
            CoinmarketCap_Coins = services.GetCoinPrices().Result;
        }



        internal async Task ExecuteLoadPortfoliosCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Portfolios.Clear();
                var items = await _repo.GetPortfoliosAsync(true);
                foreach (var item in items)
                {
                    Portfolios.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal void ExecuteLoad_ALL_PortfolioCommand()
        {

            string strtemp;
            bool coinfound;
            

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                foreach (var portfolio in Portfolios)                            // loop through each portfolio
                {
                    if (portfolio.coinsHodle.Count > 0)                          // if coins exist in portfolio
                    {
                        foreach (var coin in portfolio.coinsHodle)               // loop through each coin in portfolio
                        {
                            coinfound = false;

                            foreach (var allCoin in AllCoinsHodles)           
                           //for (int i = 0; i < AllCoinsHodles.Count; i++)        // loop through All coins in all portfolio list
                            {
                                if (allCoin.Name == coin.Name)
                                {
                                    coinfound = true;
                                    allCoin.Quantity = allCoin.Quantity + coin.Quantity;
                                    /////////
                                    ///////
                                    ////
                                    ////
                                    ////  Replace with allCoinsHodle coin
                                    ////
                                    ////
                                }
                            }

                            if (coinfound == false)
                            {
                                AllCoinsHodle coinToAdd = new AllCoinsHodle();
                                coinToAdd.Name = coin.Name;
                                coinToAdd.Quantity = coin.Quantity;
                                AllCoinsHodles.Add(coinToAdd);
                                coinToAdd = null;
                            }

                        }
                    }
                }

                foreach (var item in AllCoinsHodles)
                {
                    var cv = new AllCoinsHodleView();
                    cv.Id = item.Id;
                    cv.Name = item.Name;
                    cv.Quantity = item.Quantity;
                    cv.Total = RecalcTotal(item, getCoinPrice(item.Name));
                    strtemp = item.Name.Replace("-", "");  // removes the dash as xamarin wont allow
                    cv.imagelocation = strtemp + ".png";
                    cv.Price = getCoinPrice(item.Name);
                    AllCoinsHodlesViews.Add(cv);
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                GetTotalPortfolioValue();
                IsBusy = false;
            }

        }


        private decimal getCoinPrice(string strCoin)
        {
            Decimal decReturn = 0;

            foreach (var item in coinmarketCap_Coins)
            {
                if (strCoin == item.name)
                {
                    decReturn = (decimal)item.price;
                }
            }

            return decReturn;
        }

        internal void GetTotalPortfolioValue()
        {
            Total = 0;

            foreach (var item in AllCoinsHodlesViews)
            {
                Total += item.Total;
            }

        }

        internal decimal RecalcTotal(AllCoinsHodle coinsHodle, decimal coinPrice)
        {
            decimal decReturn = 0;
            decReturn = coinsHodle.Quantity * coinPrice;
            return Math.Round(decReturn, 2);
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

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            UpdatePrices();
            await ExecuteLoadPortfoliosCommand();
            ExecuteLoad_ALL_PortfolioCommand();
        }
    }
}
