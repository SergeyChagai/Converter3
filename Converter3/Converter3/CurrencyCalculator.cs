using System;
using System.Collections.Generic;
using System.Text;

namespace Converter3
{
    class CurrencyCalculator
    {
        private static CurrencyCalculator instance;

        public string FirstCurrency { get; set; }
        public string SecondCurrency { get; set; }
        public int FirstValue { get; set; }
        public int SecondValue { get; set; }
        private CurrencyCalculator()
        { }

        public static CurrencyCalculator getInstance()
        {
            if (instance == null)
                instance = new CurrencyCalculator();
            return instance;
        }
    }
}
