using System;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Portfolio_Coins : ContentPage
    {
        int PortfolioID;
        AddCoin addCoin;

        public Portfolio_Coins(string strPortfolioName, int intPortfolioId, PortfolioViewModel viewModel)
        {
            InitializeComponent();
            PortfolioName.Text = strPortfolioName;
            PortfolioID = intPortfolioId;

            addCoin = new AddCoin(viewModel, PortfolioID);
        }



        async void AddCoin(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(addCoin);
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

     

    }
}
