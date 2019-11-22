using System;
using System.Diagnostics;
using Cryfolio.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Cryfolio.Views
{
    public partial class Portfolio : ContentPage
    {

        PortfolioViewModel viewModel;
        NewPortfolio newPortfolio;
        bool blnErrorShown;
         

        public Portfolio()
        {
            InitializeComponent();
         

            this.BindingContext = viewModel = new PortfolioViewModel();
            newPortfolio = new NewPortfolio(viewModel);

            //PopupNavigation.Instance.Pushing += (sender, e) => Debug.WriteLine($"[Popup] Pushing: {e.Page.GetType().Name}");
            //PopupNavigation.Instance.Pushed += (sender, e) => Debug.WriteLine($"[Popup] Pushed: {e.Page.GetType().Name}");
            //PopupNavigation.Instance.Popping += (sender, e) => Debug.WriteLine($"[Popup] Popping: {e.Page.GetType().Name}");
            PopupNavigation.Instance.Popped += (sender, e) => ShowError();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Portfolios.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private async void showAddPortfolio(object o, EventArgs e)
        {
            blnErrorShown = false;
            await PopupNavigation.Instance.PushAsync(newPortfolio);       
        }

        private async void ShowError()
        {
            if (newPortfolio.Already_Exists() && blnErrorShown == false)
            {
               await PopupNavigation.Instance.PushAsync(new Alertify(newPortfolio.Portfolio_Name + " Already Exist"));
               blnErrorShown = true;

               System.Threading.Thread.Sleep(700);
               await PopupNavigation.Instance.PopAsync(true);
            }
        }

        private async void Delete(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                var imt = (Grid)button.Parent;
                var c1 = (Label)imt.Children[0];
                var name = c1.Text;

                Models.Portfolio portfolio = viewModel.GetPortfolio(viewModel.GetPortfolioID(name));

                viewModel.Delete_PortfolioAsync(portfolio);

                // MessagingCenter.Send(this, "Delete", portfolio); // need to sort this
            }
            catch(Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new Alertify("Error - " + ex.ToString()));
                System.Threading.Thread.Sleep(700);
                await PopupNavigation.Instance.PopAsync(true);
            }
       
        }

        private async void View(object sender, EventArgs e)
        {
            try
            {
                Button button = (Button)sender;
                var imt = (Grid)button.Parent;
                var c1 = (Label)imt.Children[0];
                var name = c1.Text;
                var id = viewModel.GetPortfolioID(name);

                Navigation.PushModalAsync(new Portfolio_Coins(name, id));

            }
            catch (Exception ex)
            {
                await PopupNavigation.Instance.PushAsync(new Alertify("Error - " + ex.ToString()));
                System.Threading.Thread.Sleep(700);
                await PopupNavigation.Instance.PopAsync(true);
            }

        }



        // Navigation

        void MainPage(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
        }

        void Alerts(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Alerts());
        }

        void Settings(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Settings());
        }
    }
}
