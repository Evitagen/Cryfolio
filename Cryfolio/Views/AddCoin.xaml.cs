using System;
using System.Collections.Generic;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Models.CoinMarketPortfolio;

namespace Cryfolio.Views
{
    public partial class AddCoin
     { 
        PortfolioViewModel PortfolioViewModel;
        MainViewModel coins;
        String SelectedCoin_name;
        Models.Portfolio Portfolio;
        Models.CoinsHodle CoinHodle;

        Models.CoinsHodle newCoin = new Models.CoinsHodle();
        bool blnAlready_Exists;

        public AddCoin(PortfolioViewModel ViewModel, int portfolioID)
        {
            InitializeComponent();

            coins = new MainViewModel();
            this.BindingContext = coins;
            PortfolioViewModel = ViewModel;
            Portfolio = PortfolioViewModel.GetPortfolio(portfolioID);          // gets the portfolio

        }


        void Cancel(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void Ok(object sender, System.EventArgs e)
        {


            try
            {

                //PortfolioViewModel
       

               if (!PortfolioViewModel.Coin_Exists_In_Portfolio(SelectedCoin_name))                  // Check if coin exist in Portfolio
               {
                    Console.WriteLine(SelectedCoin_name);

                    CoinHodle = new Models.CoinsHodle();

                    CoinHodle.Id = PortfolioViewModel.getNewCoinHodle_ID();
                    CoinHodle.Name = SelectedCoin_name;
                    CoinHodle.Portfolio = Portfolio;
                    PortfolioViewModel.AddCoinHodleToPortfolio(CoinHodle);


               }
               else
               {
                    // notification saying coin already exists
                    PortfolioViewModel.ShowError(SelectedCoin_name + " Already Exist");
               }

               

              




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
      
            PopupNavigation.Instance.PopAsync(true);
        }




        private void Coin_Selected(object sender, ItemTappedEventArgs e)
        {

            try
            {
                var myItem = e.Item;
                foreach (var item in coins.CoinmarketCap_Coins)
                {
                    if (myItem == item)
                    {
                        SelectedCoin_name = item.name;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
              
        }


    }
}
