using System;
using Cryfolio.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cryfolio
{
    public partial class App : Application
    {
        public static Repo Repository;
        //public static Repo_CoinsHodle CoinsHodle_Repository;
        public static CryptoRepository cryptoRepitory;

        public App(string dbPath)
        {
            InitializeComponent();

            Repository = new Repo(dbPath);
            //CoinsHodle_Repository = new Repo_CoinsHodle(dbPath);
            cryptoRepitory = new CryptoRepository(dbPath);


            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
