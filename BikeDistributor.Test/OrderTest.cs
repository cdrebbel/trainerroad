using NUnit.Framework;
using FluentAssertions;

namespace BikeDistributor.Test
{
    [TestFixture]
    public class OrderTest
    {
        private readonly static string CompanyName = "Anywhere Bike Shop";
        private const double TaxRate = .0725d;
        private Order _order;

        private string HtmlReceiptTemplate = @"<html><body><h1>Order Receipt for {5}</h1><ul><li>1 x {0} {1} = {2:C}</li></ul><h3>Sub-Total: {2:C}</h3><h3>Tax: {3:C}</h3><h2>Total: {4:C}</h2></body></html>";
        private string ReceiptTemplate = @"Order Receipt for {5}
	1 x {0} {1} = {2:C}
Sub-Total: {2:C}
Tax: {3:C}
Total: {4:C}";

        [SetUp]
        public void Setup()
        {
            _order = new Order(CompanyName, TaxRate);
        }

        [TestCase("Specialized","S-Works Venge Dura-Ace",Bike.FiveThousand, 362.50, 5362.50)]
        [TestCase("Specialized","Venge Elite",Bike.TwoThousand,145.00,2145.00)]
        [TestCase("Giant","Defy 1",Bike.OneThousand, 72.50, 1072.5)]
        public void ReceiptIsValid(string make, string model, int cost, decimal tax, decimal total)
        {
            var bike = new Bike(make, model, cost);
            _order.AddLine(new Line(bike, 1));

            var expected = string.Format(ReceiptTemplate, make, model, cost,tax,total,CompanyName);
            _order.Receipt().Should().Be(expected);
        }

        [TestCase("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand, 362.50, 5362.50)]
        [TestCase("Specialized", "Venge Elite", Bike.TwoThousand, 145.00, 2145.00)]
        [TestCase("Giant", "Defy 1", Bike.OneThousand, 72.50, 1072.5)]
        public void HtmlReceiptIsValid(string make, string model, int cost, decimal tax, decimal total)
        {
            var bike = new Bike(make, model, cost);
            _order.AddLine(new Line(bike, 1));

            var expected = string.Format(HtmlReceiptTemplate,make, model, cost, tax, total,CompanyName);
            _order.HtmlReceipt().Should().Be(expected);
        }

        [TestCase(Bike.OneThousand,1,1000)]
        [TestCase(Bike.OneThousand,100,90000)]
        [TestCase(Bike.TwoThousand, 1, 2000)]
        [TestCase(Bike.TwoThousand, 100, 160000)]
        [TestCase(Bike.FiveThousand, 1, 5000)]
        [TestCase(Bike.FiveThousand, 100, 400000)]
        public void CostIsCalculated(int price, int quantity, double expected)
        {
            var cost = _order.CalculateCost(quantity, price);
            cost.Should().Be(expected);
        }
    }
}


