using System;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Portfolio : ContentPage
    {

        PortfolioViewModel viewModel;

        public Portfolio()
        {
            InitializeComponent();
            this.BindingContext = viewModel = new PortfolioViewModel();
        }

        void MainPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Alerts(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Alerts());
        }

        void Settings(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Settings());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Portfolios.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);

            foreach (var item in viewModel.Portfolios)
            {
                Console.WriteLine(item.PortfolioName);
            }
        }

        private void showAddPortfolio(object o, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new NewPortfolio());
        }
    }
}
