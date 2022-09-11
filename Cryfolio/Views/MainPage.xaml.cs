using System;
using Cryfolio.ViewModels;
using Cryfolio.Views;
using Xamarin.Forms;

namespace Cryfolio
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {

        MainViewModel coins;

        public MainPage()
        {
            InitializeComponent();
            coins = new MainViewModel();
            this.BindingContext = coins;           
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


        void Rank_Sort(object sender, EventArgs e)
        {
            coins.SortByRank();
        }

        void Coin_Sort(object sender, EventArgs e)
        {
            coins.SortByName();
        }

        void Price_Sort(object sender, EventArgs e)
        {
            coins.SortByPrice();
        }

        void t4hr_Change(object sender, EventArgs e)
        {
            coins.Sort24();
        }

        void s7day_Change(object sender, EventArgs e)
        {
            coins.Sort7day();
        }
    }

}