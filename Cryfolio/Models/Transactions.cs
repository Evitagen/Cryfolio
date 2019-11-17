using System;
namespace Cryfolio.Models
{
    public class Transactions
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double AmountBuy { get; set; }
        public double AmountSell { get; set; }
        public virtual CoinsHodle CoinsHodle { get; set; }
    }
}
