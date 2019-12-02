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
        string Portfolio_Name;
        AddCoin addCoin;
        AddTransaction addTransaction;
        Models.CoinsHodle CoinHodle;

        public Portfolio_Coins(string strPortfolioName, int intPortfolioId, PortfolioViewModel viewModel)
        {
            InitializeComponent();
            PortfolioName.Text = strPortfolioName;
            Portfolio_Name = strPortfolioName;
            PortfolioID = intPortfolioId;
            ViewModel = viewModel;

            this.BindingContext = ViewModel = new PortfolioViewModel();

            addCoin = new AddCoin(viewModel, PortfolioID);
            

            PopupNavigation.Instance.Popped += (sender, e) => UpdateView();     // event
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel.CoinsHodles.Count == 0)
                _ = ViewModel.ExecuteLoadPortfolioCommand(PortfolioID);
        }



        async void AddCoin(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(addCoin);
        }

        async void Add_Transaction(object sender, EventArgs e)
        {

            // get coin clicked on
            Button button = (Button)sender;
            var imt = (Grid)button.Parent;
            var c1 = (Label)imt.Children[1];
            var name = c1.Text;

            foreach (var item in ViewModel.CoinsHodles)
            {
                if (item.Name == name)
                {
                    CoinHodle = item;
                }
            }

            addTransaction = new AddTransaction(ViewModel, PortfolioID, CoinHodle);

            await PopupNavigation.Instance.PushAsync(addTransaction);
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
            _ = ViewModel.ExecuteLoadPortfolioCommand(PortfolioID);
        }

    }
}
