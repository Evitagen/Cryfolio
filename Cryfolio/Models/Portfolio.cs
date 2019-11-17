using System;
using System.Collections.Generic;

namespace Cryfolio.Models
{
    public class Portfolio
    {
        public int PortfolioID { get; set; }
        public string PortfolioName { get; set; }
        public virtual ICollection<CoinsHodle> coinsHodle { get; set; }

        public static implicit operator Portfolio(int v)
        {
            throw new NotImplementedException();
        }
    }
}
