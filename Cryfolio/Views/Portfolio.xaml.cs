using System;
using Cryfolio.ViewModels;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Portfolio : ContentPage
    {
        public Portfolio()
        {
            InitializeComponent();
            this.BindingContext = new PortfolioViewModel();
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
    }
}
