using System;
using System.Collections.Generic;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class NewPortfolio 
    {
        PortfolioViewModel viewModel;
        Models.Portfolio newPortfolio = new Models.Portfolio();

        public NewPortfolio(PortfolioViewModel ViewModel)
        {
            viewModel = ViewModel;
            InitializeComponent();
        }

        void Ok(object sender, System.EventArgs e)
        {
               
            if (!viewModel.Name_Exists(PortfolioValue.Text))
            {
                newPortfolio.PortfolioName = PortfolioValue.Text;
                newPortfolio.PortfolioID = viewModel.getNewPortfolio_ID();
                MessagingCenter.Send(this, "AddItem", newPortfolio);
                PopupNavigation.Instance.PopAsync(true);
            }
        
        }

        void Cancel(object sender, System.EventArgs e)
        {
             PopupNavigation.Instance.PopAsync(true);
        }
        
    }

}