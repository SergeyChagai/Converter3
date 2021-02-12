using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        public async Task ConnectToServerAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_connectionString);
            GetValutesOffline();
        }
        public async Task GetValutes()
        { 
            XmlSerializer serializer = new XmlSerializer(typeof(ValCurs));

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Async = true;

            XmlReader reader = XmlReader.Create("C:/Users/user/source/repos/Converter3/Converter3/Converter3/Valute.cs", settings);


            //List<Valute> list = valCurs.valutes;
            //foreach (Valute valute in list)
            //{
            //    _dict.Add(valute.CharCode, valute);
            //}
        }
        public void GetValutesOffline()
        {
            _dict.Add(
                "RUB",
                new Valute
                {
                    Id = "R01235",
                    NumCode = 0,
                    CharCode = "RUB",
                    Nominal = 1,
                    Name = "Российский рубль",
                    Value = (float)1
                }
                );
            _dict.Add(
                "USD",
                new Valute
                {
                    Id = "R01235",
                    NumCode = 840,
                    CharCode = "USD",
                    Nominal = 1,
                    Name = "Доллар США",
                    Value = (float)73.9378
                }
                );
            _dict.Add(
                "EUR",
                new Valute
                {
                    Id = "R01239",
                    NumCode = 978,
                    CharCode = "EUR",
                    Nominal = 1,
                    Name = "Евро",
                    Value = (float)89.6052
                }
                );
            _dict.Add(
                "JPY",
                new Valute
                {
                    Id = "R01820",
                    NumCode = 392,
                    CharCode = "JPY",
                    Nominal = 100,
                    Name = "Японских иен",
                    Value = (float)70.4337
                }
                );
        }

        public float Calculate()
        {
            Valute from = _dict[FirstCurrency];
            Valute to = _dict[SecondCurrency];

            float result = Value * to.Nominal * from.Value / (from.Nominal * to.Value);
            return result;
        }
    }
}
