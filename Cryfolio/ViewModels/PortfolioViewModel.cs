using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Cryfolio.Models;
using Cryfolio.Services;
using Xamarin.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Command = Xamarin.Forms.Command;
using System.Linq;

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

            //MessagingCenter.Subscribe<Views.NewPortfolio, Portfolio>(this, "AddItem", async (obj, portfolio) =>
            //{
            //    var _portfolio = portfolio as Portfolio;
            //    await DataStore.AddPortfolioAsync(_portfolio);
            //    Portfolios.Add(_portfolio);
            //    await ExecuteLoadItemsCommand();
            //});

        }

       internal async void AddPortfolio(Portfolio portfolio)
       {
            var _portfolio = portfolio as Portfolio;
            await DataStore.AddPortfolioAsync(_portfolio);
            Portfolios.Add(_portfolio);
            await ExecuteLoadItemsCommand();
       }

       internal async Task ExecuteLoadItemsCommand()
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

        internal async Task Delete_PortfolioAsync(Portfolio portfolio)
        {
            Portfolios.Remove(portfolio);
            await DataStore.DeletePortfolioAsync(portfolio.PortfolioID);
        }

        internal int getNewPortfolio_ID()
        {
            int intReturn = 0;

            if (Portfolios.Count > 0)
            {
                intReturn = Portfolios.Max(x => x.PortfolioID);
                intReturn++;
            }

            return intReturn;   
        }


        internal bool Name_Exists(string name)
        {
            return Portfolios.Any(x => x.PortfolioName == name);
           
        }

        internal int GetPortfolioID(string name)
        {

            int intReturn = 0;

            Portfolio portfolio = Portfolios.Single(s => s.PortfolioName == name);
            if (portfolio != null)
            {
                intReturn = portfolio.PortfolioID;
            }
            return intReturn;
        }

        internal Portfolio GetPortfolio(int PortfolioID)
        {
            var result = (from b in Portfolios
                          where b.PortfolioID.Equals(PortfolioID)
                          select b).FirstOrDefault();
            return (Portfolio)result;
        }

    }
}
