using Converter3;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CalculatorTests
{
    public class Tests
    {
        private CurrencyCalculator calculator;
        [OneTimeSetUp]
        public void SetCalculator()
        {
            calculator = CurrencyCalculator.getInstance();
            calculator.ConnectToServerAsync();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void CalculationsTest(int caseOfMock)
        {
            InstallValues(caseOfMock);
            float actual = calculator.Calculate();
            float expected = (float)ExpectedMock(caseOfMock);
            Assert.AreEqual(expected, actual, 0.1);
        }
        private void InstallValues(int caseOfMock)
        {
            switch(caseOfMock)
            {
                case 1:
                    calculator.FirstCurrency = "RUB";
                    calculator.SecondCurrency = "USD";
                    calculator.Value = 100;
                    break;
                case 2:
                    calculator.FirstCurrency = "EUR";
                    calculator.SecondCurrency = "USD";
                    calculator.Value = 100;
                    break;
                case 3:
                    calculator.FirstCurrency = "RUB";
                    calculator.SecondCurrency = "JPY";
                    calculator.Value = 20;
                    break;
                case 4:
                    calculator.FirstCurrency = "EUR";
                    calculator.SecondCurrency = "JPY";
                    calculator.Value = 1;
                    break;
                default:
                    throw new System.Exception();
            }
        }
        private double ExpectedMock(int caseOfMock)
        {
            switch (caseOfMock)
            {
                case 1:
                    return 1.36;
                case 2:
                    return 121.20;
                case 3:
                    return 28.47;
                case 4:
                    return 127.20;
                default:
                    throw new System.Exception();
            }
        }
    }
}