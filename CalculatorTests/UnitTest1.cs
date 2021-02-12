using Converter3;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CalculatorTests
{
    public class Tests
    {
        private CurrencyCalculator calculator;
        [OneTimeSetUp]
        public async Task SetCalculator()
        {
            calculator = CurrencyCalculator.getInstance();
            await calculator.ConnectToServerAsync();
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
            Assert.AreEqual(expected, actual);
        }
        private void InstallValues(int caseOfMock)
        {
            switch(caseOfMock)
            {
                case 1:
                    calculator.FirstCurrency = "HUF";
                    calculator.SecondCurrency = "DKK";
                    calculator.Value = 1;
                    break;
                case 2:
                    calculator.FirstCurrency = "HUF";
                    calculator.SecondCurrency = "DKK";
                    calculator.Value = 1;
                    break;
                case 3:
                    calculator.FirstCurrency = "HUF";
                    calculator.SecondCurrency = "DKK";
                    calculator.Value = 1;
                    break;
                case 4:
                    calculator.FirstCurrency = "HUF";
                    calculator.SecondCurrency = "DKK";
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
                    return 0.234;
                case 2:
                    return 0.234;
                case 3:
                    return 0.234;
                case 4:
                    return 0.234;
                default:
                    throw new System.Exception();
            }
        }
    }
}