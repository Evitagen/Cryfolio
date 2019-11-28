using System;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Portfolio_Coins : ContentPage
    {
        PortfolioViewModel ViewModel;
        int PortfolioID;
        AddCoin addCoin;

        public Portfolio_Coins(string strPortfolioName, int intPortfolioId, PortfolioViewModel viewModel)
        {
            InitializeComponent();
            PortfolioName.Text = strPortfolioName;
            PortfolioID = intPortfolioId;
            ViewModel = viewModel;

            this.BindingContext = ViewModel = new PortfolioViewModel();

            addCoin = new AddCoin(viewModel, PortfolioID);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.Portfolios.Count == 0)
                ViewModel.LoadItemsCommand.Execute(null);
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
