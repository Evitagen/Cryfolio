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
        DateTime Transaction_DateTime;


        public AddTransaction(PortfolioViewModel ViewModel, int portfolioID, CoinsHodle coinsHodle, string price)
        {
            InitializeComponent();
            _coinsHodle = coinsHodle;

            PortfolioViewModel = ViewModel;
            Portfolio = PortfolioViewModel.GetPortfolio(portfolioID);

            this.BindingContext = PortfolioViewModel = new PortfolioViewModel();

            string strtemp = coinsHodle.Name.Replace("-", "");

            CoinName.Text = coinsHodle.Name;
            CoinImage.Source = strtemp + ".png";
            PriceBought.Text = price;
        }


        void Cancel(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void Ok(object sender, System.EventArgs e)
        {
            // validate all fields are fitted in

            string strError = PortfolioViewModel.ValidateTransaction_Form(Quantity.Text, Fee.Text, PriceBought.Text);
            Transaction_DateTime = PortfolioViewModel.SelectedDate + _timePicker.Time;
            Console.WriteLine(Transaction_DateTime);
                  
            if (strError.Length > 0)
            {
                PortfolioViewModel.ShowError(strError);
            }
            else
            {
                PortfolioViewModel.AddTransaction(Quantity.Text, Fee.Text, PriceBought.Text, Transaction_DateTime);   // add transaction
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
