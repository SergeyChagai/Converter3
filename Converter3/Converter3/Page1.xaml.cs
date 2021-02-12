using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Converter3
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public int Value { get; set; }
        RateViewModel viewModel;
        public Page1(string from, string to)
        {
            InitializeComponent();

            viewModel = new RateViewModel();
            // установка контекста данных
            this.BindingContext = viewModel;

            FirstCurrency = from;
            SecondCurrency = to;
            firstCurrency.Text += FirstCurrency;
            secondCurrency.Text += SecondCurrency;
        }

        private void entry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            Value = Convert.ToInt32(entry.Text);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(entry.Text))
            {
                DisplayAlert("Ошибка", "Введите номинал", "OK");
            }
            if (Convert.ToInt32(entry.Text) < 0)
            {
                DisplayAlert("Ошибка", "Ddtlbnt положительное число", "OK");
            }
            else
                    {
                CurrencyCalculator calculator = CurrencyCalculator.getInstance();
                calculator.Value = Value;
                float reponse = calculator.Calculate();
                result.Text = Convert.ToString(reponse);
            }
        }
    }
}