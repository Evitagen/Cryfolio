using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoinMarketPortfolio;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Cryfolio
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    //[DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            _ = GetCoinsAsync();
        }

        private async Task GetCoinsAsync()
        {


            HttpClient client = new HttpClient();

            var response = await client.GetStringAsync("https://cryfolio.azurewebsites.net/api/coins");

            var coins = JsonConvert.DeserializeObject<List<Coins>>(response);
            var coinsFormated = new List<Coins>();
            string strtemp;


            foreach (var coin in coins)
            {
                if (coin.price < 1) {
                    coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 6);   // 8 dec places
                }

                if (coin.price > 1 && coin.price < 100) {
                    coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 4);   // 4 dec places
                }

                if (coin.price > 100) {
                   coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 2);    // 2 decimal
                }

                strtemp = coin.name.Replace("-", "") ;  // removes the dash as xamarin wont allow
                coin.imagelocation = strtemp + ".png";

                if (coin.PercentChange7day > 0) {
                    coin.PercentChange7dayColor = "LightGreen";
                } else {
                    coin.PercentChange7dayColor = "Red";
                }

                if (coin.PercentChange24hr > 0)
                {
                    coin.PercentChange24hrColor = "LightGreen";
                } else {
                    coin.PercentChange24hrColor = "Red";
                }
                

                coinsFormated.Add(coin);
            }


            CoinsListView.ItemsSource = coinsFormated;

            client.Dispose();

            
        }
    }
}

