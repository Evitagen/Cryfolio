using System;
using System.Collections.Generic;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class AddCoin
     { 
        PortfolioViewModel ViewModel;
        //String CoinName = "hello";


        Models.CoinsHodle newCoin = new Models.CoinsHodle();
        bool blnAlready_Exists;

        public AddCoin(PortfolioViewModel ViewModel)
        {
            InitializeComponent();
            ViewModel = ViewModel;
        }


    }
}
