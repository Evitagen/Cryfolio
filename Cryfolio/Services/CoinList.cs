using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Models.CoinMarketPortfolio;
using Newtonsoft.Json;


namespace Cryfolio.Services
{
    public class CoinList 
    {
   
        public async Task<List<Coins>> GetCoinPrices()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://cryfolio.azurewebsites.net/api/coins").ConfigureAwait(false);
            client.Dispose();

            var coins = JsonConvert.DeserializeObject<List<Coins>>(response.ToString());
            var coinsFormated = new List<Coins>();
            string strtemp;


              foreach (var coin in coins)
              {
                    if (coin.price < 1)
                    {
                        coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 6);   // 8 dec places
                    }

                    if (coin.price > 1 && coin.price < 100)
                    {
                        coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 4);   // 4 dec places
                    }

                    if (coin.price > 100)
                    {
                        coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 2);   // 2 decimal
                    }

                    strtemp = coin.name.Replace("-", "");                                    // removes the dash as xamarin wont allow
                    coin.imagelocation = strtemp + ".png";

                    if (coin.PercentChange7day > 0)
                    {
                        coin.PercentChange7dayColor = "LightGreen";
                    }
                    else
                    {
                        coin.PercentChange7dayColor = "Red";
                    }

                    if (coin.PercentChange24hr > 0)
                    {
                        coin.PercentChange24hrColor = "LightGreen";
                    }
                    else
                    {
                        coin.PercentChange24hrColor = "Red";
                    }

                    coinsFormated.Add(coin);
              }

            return coinsFormated;
        }
    }
}
