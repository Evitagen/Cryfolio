using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Alerts : ContentPage
    {
        public Alerts()
        {
            InitializeComponent();
        }

        void MainPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Settings(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Settings());
        }

        void Portfolio(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Portfolio());
        }
    }
}
