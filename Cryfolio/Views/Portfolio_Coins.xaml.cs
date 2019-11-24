using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Portfolio_Coins : ContentPage
    {
        int PortfolioID;

        public Portfolio_Coins(string strPortfolioName, int intPortfolioId)
        {
            InitializeComponent();
            PortfolioName.Text = strPortfolioName;
            PortfolioID = intPortfolioId;
        }

        void MainPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Portfolio(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Portfolio());
        }

        void Alerts(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Alerts());
        }

        void Settings(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Settings());
        }

        void AddCoin(object sender, EventArgs e)
        {
            Console.WriteLine("Add Coin");
        }

    }
}
