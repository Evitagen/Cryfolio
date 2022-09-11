using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        void MainPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Alerts(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Alerts());
        }

        void Portfolio(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Portfolio());
        }

    }
}
