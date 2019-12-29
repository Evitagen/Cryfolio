using System;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class All_Portfolio_Coins : ContentPage
    {
        AllPortfolioViewModel ViewModel;
        int PortfolioID;
        string Portfolio_Name;
        AddCoin addCoin;
        AddTransaction addTransaction;
        Models.CoinsHodle CoinHodle;

        public All_Portfolio_Coins(string strPortfolioName, int intPortfolioId, AllPortfolioViewModel viewModel)
        {
            InitializeComponent();
            AllPortfolioName.Text = strPortfolioName;

            Portfolio_Name = strPortfolioName;
            PortfolioID = intPortfolioId;
            ViewModel = viewModel;

            this.BindingContext = ViewModel = new AllPortfolioViewModel();

           // addCoin = new AddCoin(viewModel, PortfolioID);


            PopupNavigation.Instance.Popped += (sender, e) => UpdateView();     // event
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (ViewModel.CoinsHodles.Count == 0)
            //    _ = ViewModel.ExecuteLoadPortfolioCommand(PortfolioID);
            //Total.Text = "$" + ViewModel.Total.ToString();
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

        private void UpdateView()
        {
            //_ = ViewModel.ExecuteLoadPortfolioCommand(PortfolioID);
            //Total.Text = "$" + ViewModel.Total.ToString();
        }

    }
}
