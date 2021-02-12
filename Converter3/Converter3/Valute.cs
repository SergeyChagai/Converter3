using System;
using System.Collections.Generic;
using System.Text;

namespace Converter3
{
    [Serializable]
    public class Valute
    {
        public string Id { get; set; }
        public int NumCode { get; set; }
        public string CharCode { get; set; }
        public int Nominal { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
    }
}
