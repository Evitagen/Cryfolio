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
        int intPortfolioId = 0;

        public NewPortfolio(PortfolioViewModel ViewModel)
        {
            viewModel = ViewModel;
            InitializeComponent();
        }



        void Ok(object sender, System.EventArgs e)
            {
                PopupNavigation.Instance.PopAsync(true);


                // gets the next new id number
                if (viewModel.Portfolios.Count == 0)
                {
                    viewModel.LoadItemsCommand.Execute(null);
                    if (viewModel.Portfolios.Count > 0)
                    intPortfolioId = viewModel.getNewPortfolio_ID();
                }

                newPortfolio.PortfolioName = PortfolioValue.Text;
                newPortfolio.PortfolioID = intPortfolioId;   
                //newPortfolio.coinsHodle = 
                

                MessagingCenter.Send(this, "AddItem", newPortfolio);
            }

            void Cancel(object sender, System.EventArgs e)
            {
                PopupNavigation.Instance.PopAsync(true);
            }
        
    }

}