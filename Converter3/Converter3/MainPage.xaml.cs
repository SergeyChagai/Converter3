using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Converter3
{
    public partial class MainPage : ContentPage
    {
        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public MainPage()
        {
            InitializeComponent();
            Connect();
        }
        private async void Connect()
        {
            CurrencyCalculator calculator = CurrencyCalculator.getInstance();
            await calculator.ConnectToServerAsync();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(FirstCurrency) || String.IsNullOrEmpty(SecondCurrency))
            {
                DisplayAlert("Ошибка", "Выберите валюты", "OK");
            }
            else
            {
                CurrencyCalculator calculator = CurrencyCalculator.getInstance();
                calculator.FirstCurrency = FirstCurrency;
                calculator.SecondCurrency =  SecondCurrency;
                Navigation.PushModalAsync(new Page1(FirstCurrency, SecondCurrency));
            }
        }

        private void picker1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FirstCurrency = Convert.ToString(picker1.SelectedItem);
        }

        private void picker2_SelectedIndexChanged(object sender, EventArgs e)
        {
            SecondCurrency = Convert.ToString(picker2.SelectedItem);
        }
        public void CheckConnection()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.None)
            {
                errorLabel.Text = "Подключение отсутствует";
            }
            else errorLabel.Text = "OK";
        }
    }
}
