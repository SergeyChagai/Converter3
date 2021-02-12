using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Xml.Serialization;

namespace Converter3
{
    public class CurrencyCalculator
    {
        private static CurrencyCalculator instance;

        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public int Value { get; set; }

        private string _connectionString = "https://www.cbr-xml-daily.ru/daily_utf8.xml";
        private Dictionary<string, Valute> _dict;
        private CurrencyCalculator()
        {
            _dict = new Dictionary<string, Valute>();
        }

        public static CurrencyCalculator getInstance()
        {
            if (instance == null)
                instance = new CurrencyCalculator();
            return instance;
        }
        public async System.Threading.Tasks.Task ConnectToServerAsync()
        {
            HttpClient client = new HttpClient(); 
            HttpResponseMessage response = await client.GetAsync(_connectionString);
            XmlSerializer serializer = new XmlSerializer(typeof(Valute));
            ValCurs valCurs = (ValCurs)serializer.Deserialize(await response.Content.ReadAsStreamAsync());
            List<Valute> list = valCurs.valutes;
            foreach (Valute valute in list)
            {
                _dict.Add(valute.CharCode, valute);
            }
        }
        public float Calculate()
        {
            Valute from = _dict[FirstCurrency];
            Valute to = _dict[SecondCurrency];

            float result = Value * from.Nominal * to.Value / (to.Nominal * from.Value);
            return result;
        }
    }
}
