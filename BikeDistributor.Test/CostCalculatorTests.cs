using NUnit.Framework;
using FluentAssertions;

namespace BikeDistributor.Test
{
    [TestFixture]
    public class CostCalculatorTests
    {
        [TestCase(1000, 1, 1000)]
        [TestCase(1000, 100, 90000)]
        [TestCase(2000, 1, 2000)]
        [TestCase(2000, 100, 160000)]
        [TestCase(5000, 1, 5000)]
        [TestCase(5000, 100, 400000)]
        public void CostIsCalculated(decimal price, int quantity, decimal expected)
        {
            var cost = new CostCalculator().CalculateCost(quantity, price);
            cost.Should().Be(expected);
        }
    }
}
