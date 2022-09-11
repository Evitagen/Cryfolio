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
        int PortfolioID;


        public AddTransaction(PortfolioViewModel ViewModel, int portfolioID, CoinsHodle coinsHodle, string price)
        {
            InitializeComponent();
            _coinsHodle = coinsHodle;

            PortfolioViewModel = ViewModel;
            _ = PortfolioViewModel.ExecuteLoadPortfoliosCommand();

            Portfolio = PortfolioViewModel.GetPortfolio(portfolioID);
            PortfolioID = portfolioID;

            this.BindingContext = PortfolioViewModel = new PortfolioViewModel();

            string strtemp = coinsHodle.Name.Replace("-", "");

            CoinName.Text = coinsHodle.Name;
            CoinImage.Source = strtemp + ".png";
            PriceBought.Text = price;

            Buy();
        }


        void Cancel(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void Ok(object sender, System.EventArgs e)
        {
            // validate all fields are fitted in

            try
            {
                string strError = PortfolioViewModel.ValidateTransaction_Form(Quantity.Text, Fee.Text, PriceBought.Text);
                Transaction_DateTime = PortfolioViewModel.SelectedDate + _timePicker.Time;
                Console.WriteLine(Transaction_DateTime);

                if (strError.Length > 0)
                {
                    PortfolioViewModel.ShowError(strError);
                }
                else
                {
                    PortfolioViewModel.AddTransaction(Portfolio, _coinsHodle, Quantity.Text, Fee.Text, PriceBought.Text, Transaction_DateTime);   // add transaction
                }

                // add up all transactions and update CoinsHodle
                PopupNavigation.Instance.PopAsync(true);
            }
            catch (Exception ex)
            {
                PortfolioViewModel.ShowError(ex.ToString());
            }
         
        }


        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
           // Console.WriteLine(PortfolioViewModel.SelectedDate);

           
        }

       

        ColorTypeConverter converter = new ColorTypeConverter();

        void Buy(object sender, System.EventArgs e)
        {
            Buy();
        }

        void Sell(object sender, System.EventArgs e)
        {
            PortfolioViewModel.Buy = false;

            Background.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            coin.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            BuySell.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            gridQuantity.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            TransactionFee.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            Date.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            Time.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            gridPriceBought.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#1a0000"));
            OkCancel.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));

            btnSell.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));
            btnBuy.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#999999"));
            lblBuySell.TextColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));
            lblQuantity.TextColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));
            lblFee.TextColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));
            lblDate.TextColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));
            lblTime.TextColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));
            lblPriceBought.TextColor = (Color)(converter.ConvertFromInvariantString("#ff6666"));

        }

        void Buy()
        {
            PortfolioViewModel.Buy = true;

            Background.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            coin.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            BuySell.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            gridQuantity.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            TransactionFee.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            Date.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            Time.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            gridPriceBought.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));
            OkCancel.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#001a00"));

            btnBuy.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
            btnSell.BackgroundColor = (Color)(converter.ConvertFromInvariantString("#999999"));
            lblBuySell.TextColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
            lblQuantity.TextColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
            lblFee.TextColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
            lblDate.TextColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
            lblTime.TextColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
            lblPriceBought.TextColor = (Color)(converter.ConvertFromInvariantString("#4dff88"));
        }


    }
}
