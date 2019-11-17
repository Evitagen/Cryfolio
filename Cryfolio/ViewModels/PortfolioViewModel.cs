using System;
using Cryfolio.Models;
using Cryfolio.Services;

namespace Cryfolio.ViewModels
{
    public class PortfolioViewModel : ViewModelBase
    {

        public IDataStore<Portfolio> DataStore => App.Repository;
    }
}
