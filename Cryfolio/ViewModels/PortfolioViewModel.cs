using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Cryfolio.Models;
using Cryfolio.Services;
using Xamarin.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Command = Xamarin.Forms.Command;
using System.Linq;
using System.Collections.Generic;
using Models.CoinMarketPortfolio;
using System.Timers;

namespace Cryfolio.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {

    
        public ObservableCollection<Portfolio> Portfolios { get; set; }
        public ObservableCollection<CoinsHodle> CoinsHodles { get; set; }
        public ObservableCollection<CoinsHodlesView> CoinsHodlesViews { get; set; }
        public ObservableCollection<Transactions> Transactions { get; set; }

        public DateTime SelectedDate { get; set; }
        public bool Buy { get; set; }
        public decimal Total { get; set; }

        private readonly ICryptoRepository _repo;
        private static System.Timers.Timer aTimer;
        private int portfolioID = 0;

        public Command LoadItemsCommand { get; set; }

        public PortfolioViewModel()
        {
            Title = "Browse";
            Portfolios = new ObservableCollection<Portfolio>();
            CoinsHodles = new ObservableCollection<CoinsHodle>();
            CoinsHodlesViews = new ObservableCollection<CoinsHodlesView>();
            Transactions = new ObservableCollection<Transactions>();

            SelectedDate = DateTime.Now;
       

            UpdatePrices();
            aTimer = new System.Timers.Timer(120000);  // every 120 seconds 2 min 
            Timer();

            _repo = CryptoRepository;
            LoadItemsCommand = new Command(async () => await ExecuteLoadPortfoliosCommand());


            //MessagingCenter.Subscribe<Views.NewPortfolio, Portfolio>(this, "AddItem", async (obj, portfolio) =>
            //{
            //    var _portfolio = portfolio as Portfolio;
            //    await DataStore.AddPortfolioAsync(_portfolio);
            //    Portfolios.Add(_portfolio);
            //    await ExecuteLoadItemsCommand();
            //});

        }

       internal async void AddPortfolio(Portfolio portfolio)
       {
            var _portfolio = portfolio as Portfolio;
            await _repo.AddPortfolioAsync(_portfolio);
            Portfolios.Add(_portfolio);
            await ExecuteLoadPortfoliosCommand();
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

        internal async Task ExecuteLoadPortfolioCommand(int PortfolioID)
        {
            portfolioID = PortfolioID;
            string strtemp;

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CoinsHodles.Clear();
                CoinsHodlesViews.Clear();
                var portfolio = await _repo.GetPortfolioAsync(PortfolioID.ToString());
                foreach (var item in portfolio.coinsHodle)
                {
                    CoinsHodles.Add(item);
                }

                foreach (var item in CoinsHodles)
                {
                    var cv = new CoinsHodlesView();
                    cv.Id = item.Id;
                    cv.Name = item.Name;
                    cv.Quantity = item.Quantity;
                    cv.Total = RecalcTotal(item, getCoinPrice(item.Name));
                    strtemp = item.Name.Replace("-", "");  // removes the dash as xamarin wont allow
                    cv.imagelocation = strtemp + ".png";
                    cv.Price = getCoinPrice(item.Name);
                    CoinsHodlesViews.Add(cv);
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

        internal async Task Delete_PortfolioAsync(Portfolio portfolio)
        {
            Portfolios.Remove(portfolio);
            await _repo.DeletePortfolioAsync(portfolio.PortfolioID);
        }

        internal int getNewPortfolio_ID()
        {
            int intReturn = 0;

            if (Portfolios.Count > 0)
            {
                intReturn = Portfolios.Max(x => x.PortfolioID);
                intReturn++;
            }

            return intReturn;   
        }


        internal bool Name_Exists(string name)
        {
            return Portfolios.Any(x => x.PortfolioName == name);
           
        }

        internal int GetPortfolioID(string name)
        {

            int intReturn = 0;

            Portfolio portfolio = Portfolios.Single(s => s.PortfolioName == name);
            if (portfolio != null)
            {
                intReturn = portfolio.PortfolioID;
            }
            return intReturn;
        }

        internal Portfolio GetPortfolio(int PortfolioID)
        {
            var result = (from b in Portfolios
                          where b.PortfolioID.Equals(PortfolioID)
                          select b).FirstOrDefault();
            return (Portfolio)result;
        }

        internal void GetTotalPortfolioValue()
        {
            Total = 0;
         
            foreach (var item in CoinsHodlesViews)
            {
                Total += item.Total;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="CoinsHodle"></param>
        ///

        internal async void AddCoinHodleToPortfolio(CoinsHodle coinHodle, Portfolio portfolio)
        {
            CoinsHodles.Add(coinHodle);
            await _repo.AddCoinsHodleAsync(coinHodle);
            await ExecuteLoadPortfoliosCommand();
        }


        internal bool Coin_Exists_In_Portfolio(Portfolio portfolio, string name)     //
        {                                                                            // TODO: replace with linq
            bool blnReturn = false;

            if (portfolio.coinsHodle != null)
            {
                foreach (var coinsHodle in portfolio.coinsHodle)
                {
                    if (coinsHodle.Name == name)
                    {
                        blnReturn = true;
                    }
                }
            }

            return blnReturn;

        }

        internal int getNewCoinHodle_ID()
        {
            int intReturn = 0;

            if (CoinsHodles.Count > 0)
            {
                intReturn = CoinsHodles.Max(x => x.Id);
                intReturn++;
            }

            return intReturn;
        }


        internal async void ShowError(string errorText)
        {
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PushAsync(new Cryfolio.Views.Alertify(errorText));
            System.Threading.Thread.Sleep(700);
            await Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync(true);
        }





        /// <summary>
        ///  CoinMarketCap coins
        /// </summary>
        ///

        public void UpdatePrices()
        {
            var services = new CoinList();
            CoinmarketCap_Coins = services.GetCoinPrices().Result;
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
            await ExecuteLoadPortfolioCommand(portfolioID);
        }




        ///
        ///  Transactions
        ///
        ///
        ///
        ///


        // Validation
        internal string ValidateTransaction_Form(string Quantity, string TransactionFee, string PriceBought)
        {


            string strErrorReturn = "";


            // Quantity
            if (Quantity == null)
            {
                strErrorReturn = "Enter an amount for quantity " + System.Environment.NewLine;
            }
            else
            {
                decimal value;
                if (Decimal.TryParse(Quantity, out value)) // It's a decimal
                {
                    if (decimal.Parse(Quantity) <= 0)
                    {
                        strErrorReturn = "Quantity must be a positive number";
                    }
                }
                else
                {
                    strErrorReturn = "must be a number";
                }
            }


            // Transaction

            if (TransactionFee == null)
            {
                strErrorReturn = "Enter an amount for Transaction Fee " + System.Environment.NewLine;
            }
            else
            {
                decimal value;
                if (Decimal.TryParse(TransactionFee, out value)) // It's a decimal
                {
                    if (decimal.Parse(TransactionFee) < 0)
                    {
                        strErrorReturn = "Transaction Fee must not be negative";
                    }
                }
                else
                {
                    strErrorReturn = "Transaction Fee must be a number";
                }
            }


            // Price Bought

            if (PriceBought == null)
            {
                strErrorReturn = "Enter an amount for Price Bought " + System.Environment.NewLine;
            }
            else
            {
                decimal value;
                if (Decimal.TryParse(PriceBought, out value)) // It's a decimal
                {
                    if (decimal.Parse(PriceBought) <= 0)
                    {
                        strErrorReturn = "Price Bought must be a positive number";
                    }
                }
                else
                {
                    strErrorReturn = "Price Bought must be a number";
                }
            }

            return strErrorReturn;
        }

        internal int getNewTransaction_ID()
        {
            int intReturn = 0;

            if (Transactions.Count > 0)
            {
                intReturn = Transactions.Max(x => x.Id);
                intReturn++;
            }

            return intReturn;
        }


         internal async void AddTransaction(Portfolio portfolio, CoinsHodle coinsHodle, string Quantity, string Fee, string PriceBought, DateTime datetime)
        {
     

            var Transaction = new Models.Transactions();

            Transaction.Id = getNewTransaction_ID();

            if (Buy)
            {
                Transaction.AmountBuy = Decimal.Parse(Quantity);
            }
            else
            {
                Transaction.AmountSell = Decimal.Parse(Quantity);
            }

            Transaction.Date = datetime;
            Transaction.TransactionFee = Decimal.Parse(Fee);
            Transaction.PriceBought = Decimal.Parse(PriceBought);
            Transaction.CoinsHodle = coinsHodle;

            AddTransactionToDb(Transaction, portfolio, coinsHodle);


            await ExecuteLoadPortfolioCommand(portfolio.PortfolioID); // update loadportfolios May need to change to LoadPortfolios
           
        }

        internal async void AddTransactionToDb(Transactions transacton, Portfolio Portfolio, CoinsHodle coinsHodle)
        {
            Transactions.Add(transacton);
            await _repo.AddTransactionAsync(transacton);                // adds transaction to db

            ExecuteLoadTransactionsForPortfolio(Portfolio, coinsHodle); // gets all transactions in current portfolio

            
            decimal total = 0;
            foreach (var transaction in coinsHodle.Transactions)        // loop through all transactions for that coin in
            {
                if (transaction.AmountBuy > 0)
                {
                    total += transaction.AmountBuy;
                }
                if (transaction.AmountSell > 0)
                {
                    total -= transaction.AmountSell;
                }
            }

            coinsHodle.Quantity = total;

            await _repo.UpdateCoinsHodleAsync(coinsHodle);              // updates total of all transactions combined to coinsHodle

           
            // RecalcTotals(coinHodle)

        }


        internal decimal RecalcTotal(CoinsHodle coinsHodle, decimal coinPrice)
        {
            decimal decReturn = 0;
            decReturn = coinsHodle.Quantity * coinPrice;
            return decReturn;
        }

        internal async Task ExecuteLoadTransactionsForPortfolio(Portfolio Portfolio, CoinsHodle coinsHodle)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Transactions.Clear();
                var trans = await _repo.GetTransactionsAsync(true);
                foreach (var tran in trans)
                {
                    if (tran.CoinsHodle == coinsHodle) //  && (coinsHodle.Portfolio == Portfolio)
                    {
                        Transactions.Add(tran);
                    }             
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


    }
}
