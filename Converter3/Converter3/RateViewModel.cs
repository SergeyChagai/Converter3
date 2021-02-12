using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Converter3
{
    class RateViewModel : INotifyPropertyChanged
    {
        private string id;
        private int numCode;
        private string charCode;
        private int nominal;
        private string name;
        private float value;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public int NumCode
        {
            get { return numCode; }
            set
            {
                numCode = value;
                OnPropertyChanged("NumCode");
            }
        }
        public string CharCode
        {
            get { return charCode; }
            set
            {
                charCode = value;
                OnPropertyChanged("CharCode");
            }
        }
        public int Nominal
        {
            get { return nominal; }
            set
            {
                nominal = value;
                OnPropertyChanged("Nominal");
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public float Value
        {
            get { return this.value; }
            set
            {
                this.value = value;
                OnPropertyChanged("Value");
            }
        }


        public ICommand LoadDataCommand { protected set; get; }

        public RateViewModel()
        {
            this.LoadDataCommand = new Command(LoadData);
        }

        private async void LoadData()
        {
            string url = "https://query.yahooapis.com/v1/public/yql?q=select+*+from+yahoo.finance.xchange+where+pair+=+%22USDRUB%22&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys&callback=";

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);
                var response = await client.GetAsync(client.BaseAddress);
                response.EnsureSuccessStatusCode(); // выброс исключения, если произошла ошибка

                // десериализация ответа в формате json
                var content = await response.Content.ReadAsStringAsync();
                JObject o = JObject.Parse(content);

                var str = o.SelectToken(@"$.data");
                var rateInfo = JsonConvert.DeserializeObject<Valute>(str.ToString());

                this.Id = rateInfo.Id;
                this.NumCode = rateInfo.NumCode;
                this.CharCode = rateInfo.CharCode;
                this.Nominal = rateInfo.Nominal;
                this.Name = rateInfo.Name;
                this.Value = rateInfo.Value;
            }
            catch (Exception ex)
            { }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
