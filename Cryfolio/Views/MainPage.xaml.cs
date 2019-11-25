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
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainViewModel();
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


        void Coin_Sort(object sender, EventArgs e)
        {
        
        }

        void Price_Sort(object sender, EventArgs e)
        {
            
        }

        void t4hr_Change(object sender, EventArgs e)
        {
           
        }

        void s7day_Change(object sender, EventArgs e)
        {
         
        }
    }

}