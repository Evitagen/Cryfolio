using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Alerts : ContentPage
    {
        AddAlert addAlert;

        public Alerts()
        {
            InitializeComponent();

            addAlert = new AddAlert();
        }


        private async void AddAlert(object o, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(addAlert);
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
