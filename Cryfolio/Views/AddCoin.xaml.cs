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
        int PortfolioID;

        Models.CoinsHodle newCoin = new Models.CoinsHodle();
        bool blnAlready_Exists;

        public AddCoin(PortfolioViewModel ViewModel, int portfolioID)
        {
            InitializeComponent();

            coins = new MainViewModel();
            this.BindingContext = coins;
            PortfolioViewModel = ViewModel;
            PortfolioID = portfolioID;

        }


        void Cancel(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void Ok(object sender, System.EventArgs e)
        {
            //var imt = ListView.SelectedItemProperty.PropertyName();

            try
            {
                Console.WriteLine(SelectedCoin_name);

                //PortfolioViewModel
                //PortfolioID


                // Check if coin exist in Portfolio

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