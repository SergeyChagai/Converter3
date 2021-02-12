using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Converter3
{
    class RateViewModel : INotifyPropertyChanged
    {
        public float FirstCurrencyRate { get; set; }
        public float SecondCurrencyRate { get; set; }

        public async Task LoadData(string from, string to)
        {
            string url = "https://currate.ru/api/?get=rates";
            url += $"&pairs={from}RUB,{to}RUB&key=701ba922f372a74ffd0512f881d8ddfb";
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
                var rateInfo = JsonConvert.DeserializeObject<Rate>(str.ToString());

                this.FirstCurrencyRate = rateInfo.FirstCurrencyRate;
                this.SecondCurrencyRate = rateInfo.SecondCurrencyRate;
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
