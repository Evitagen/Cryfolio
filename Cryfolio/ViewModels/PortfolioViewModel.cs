using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Cryfolio.Models;
using Cryfolio.Services;
using Xamarin.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Command = Xamarin.Forms.Command;

namespace Cryfolio.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {

    
        public ObservableCollection<Portfolio> Portfolios { get; set; }
        public Command LoadItemsCommand { get; set; }

        public PortfolioViewModel()
        {
            Title = "Browse";
            Portfolios = new ObservableCollection<Portfolio>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<Views.NewPortfolio, Portfolio>(this, "AddItem", async (obj, portfolio) =>
            {
                var _portfolio = portfolio as Portfolio;
                Portfolios.Add(_portfolio);
                await DataStore.AddPortfolioAsync(_portfolio);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Portfolios.Clear();
                var items = await DataStore.GetPortfoliosAsync(true);
                foreach (var item in items)
                {
                    Portfolios.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal int getNewPortfolio_ID()
        {
            int intReturn = 0;

            if (Portfolios.Count > 0)
            {
                foreach (var portfolio in Portfolios)
                {
                    if (portfolio.PortfolioID >= intReturn)
                    {
                        intReturn = portfolio.PortfolioID;
                        intReturn++;
                    }
                }
            }

            return intReturn;   
        }


    }
}
