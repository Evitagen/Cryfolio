using System;
using System.Collections.Generic;
using Cryfolio.Models;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class AddTransaction 
    {
        CoinsHodle _coinsHodle;
        PortfolioViewModel PortfolioViewModel;
        Models.Portfolio Portfolio;
        Models.Transactions Transaction;

        public AddTransaction(PortfolioViewModel ViewModel, int portfolioID, CoinsHodle coinsHodle)
        {
            InitializeComponent();
            _coinsHodle = coinsHodle;
            PortfolioViewModel = ViewModel;
            Portfolio = PortfolioViewModel.GetPortfolio(portfolioID);

            this.BindingContext = PortfolioViewModel = new PortfolioViewModel();
        }


        void Cancel(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void Ok(object sender, System.EventArgs e)
        {
            // validate all fields are fitted in

      

            string strError = PortfolioViewModel.ValidateTransaction_Form(Quantity.Text, Fee.Text, PriceBought.Text);

            Console.WriteLine(PortfolioViewModel.SelectedDate);
            // Console.WriteLine(PortfolioViewModel.SelectedTime);
            // add transaction


            if (strError.Length > 0)
            {
                // show popup error
            }
            else
            {
                Transaction = new Models.Transactions();


                //CoinHodle.Id = PortfolioViewModel.getNewCoinHodle_ID();
                //CoinHodle.Name = SelectedCoin_name;
                //CoinHodle.Portfolio = Portfolio;
                //PortfolioViewModel.AddCoinHodleToPortfolio(CoinHodle, Portfolio);

            }





            // add up all transactions and update CoinsHodle

            PopupNavigation.Instance.PopAsync(true);
        }


        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
           // Console.WriteLine(PortfolioViewModel.SelectedDate);

           
        }

  
    }
}
