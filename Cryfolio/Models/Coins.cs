using System;
namespace Models.CoinMarketPortfolio
{
    public class Coins
    {

        public string name { get; set; }
        public double price { get; set; }
        public double volume { get; set; }
        public double circulating { get; set; }
        public double marketcap { get; set; }
        public string imagelocation { get; set; }
        public decimal TotalSupply { get; set; }
        public decimal PercentChange1hr { get; set; }
        public decimal PercentChange24hr { get; set; }
        public string PercentChange24hrColor { get; set; }
        public decimal PercentChange7day { get; set; }
        public string PercentChange7dayColor { get; set; }
        public int CoinMcapRank { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}