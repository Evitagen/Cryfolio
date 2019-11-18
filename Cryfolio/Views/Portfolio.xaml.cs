using System;
using Cryfolio.ViewModels;
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
    }
}
