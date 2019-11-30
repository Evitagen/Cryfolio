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

namespace Cryfolio.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {

    
        public ObservableCollection<Portfolio> Portfolios { get; set; }
        public ObservableCollection<CoinsHodle> CoinsHodles { get; set; }
        public ObservableCollection<CoinsHodlesView> CoinsHodlesViews { get; set; }

        private readonly ICryptoRepository _repo;

        public Command LoadItemsCommand { get; set; }

        public PortfolioViewModel()
        {
            Title = "Browse";
            Portfolios = new ObservableCollection<Portfolio>();
            CoinsHodles = new ObservableCollection<CoinsHodle>();
            CoinsHodlesViews = new ObservableCollection<CoinsHodlesView>();

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
                    strtemp = item.Name.Replace("-", "");  // removes the dash as xamarin wont allow
                    cv.imagelocation = strtemp + ".png";
                    // cv.Price = 
                    CoinsHodlesViews.Add(cv);
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









    }
}
