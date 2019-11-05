using System;
using System.Collections.Generic;
using System.ComponentModel;
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

            var response = await client.GetStringAsync("http://178.128.168.19/api/coins");

            var coins = JsonConvert.DeserializeObject<List<Coins>>(response);

            CoinsListView.ItemsSource = coins;

            client.Dispose();

            // jlj
        }
    }
}

