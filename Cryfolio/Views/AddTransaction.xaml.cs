using System;
using System.Collections.Generic;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class AddTransaction 
    {
        PortfolioViewModel PortfolioViewModel;
        Models.Portfolio Portfolio;

        public AddTransaction(PortfolioViewModel ViewModel, int portfolioID)
        {
            InitializeComponent();

            PortfolioViewModel = ViewModel;
            Portfolio = PortfolioViewModel.GetPortfolio(portfolioID);
        }


        void Cancel(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }

        void Ok(object sender, System.EventArgs e)
        {
            PopupNavigation.Instance.PopAsync(true);
        }



    }
}
