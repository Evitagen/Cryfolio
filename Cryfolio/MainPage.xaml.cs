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
                if (coin.price < 1)
                {
                    // 8 dec places
                    coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 6);
                }

                if (coin.price > 1 && coin.price < 100)
                {
                    // 4 dec places
                    coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 4);
                }

                if (coin.price > 100)
                {
                    // 2 decimal
                   coin.price = (double)Math.Round(Convert.ToDecimal(coin.price), 2);
                }

                strtemp = coin.name.Replace("-", "") ;  // removes the dash as xamarin wont allow

                coin.imagelocation = strtemp + ".png";
                coinsFormated.Add(coin);

                if (File.Exists(coin.imagelocation))
                {
                    Console.WriteLine("file exists!!");
                }
                else
                {
                    Console.WriteLine("NO");
                }
            }



            CoinsListView.ItemsSource = coinsFormated;

            client.Dispose();

            // jlj
        }
    }
}

