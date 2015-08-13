using NUnit.Framework;
using FluentAssertions;

namespace BikeDistributor.Test
{
    [TestFixture]
    public class OrderTest
    {
        //TODO: Mock CalculateCost and inject into new Line()
        private readonly static string CompanyName = "Anywhere Bike Shop";
        private const decimal TaxRate = .0725m;
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

        [TestCase("Specialized","S-Works Venge Dura-Ace",5000.00, 362.50, 5362.50)]
        [TestCase("Specialized","Venge Elite",2000.00,145.00,2145.00)]
        [TestCase("Giant","Defy 1",1000.00, 72.50, 1072.5)]
        public void ReceiptIsValid(string make, string model, decimal cost, decimal tax, decimal total)
        {
            var bike = new Bike(make, model, cost);
            _order.AddLine(new Line(bike, 1));

            var expected = string.Format(ReceiptTemplate, make, model, cost,tax,total,CompanyName);
            _order.Receipt().Should().Be(expected);
        }

        [TestCase("Specialized", "S-Works Venge Dura-Ace", 5000.00, 362.50, 5362.50)]
        [TestCase("Specialized", "Venge Elite", 2000.00, 145.00, 2145.00)]
        [TestCase("Giant", "Defy 1", 1000.00, 72.50, 1072.5)]
        public void HtmlReceiptIsValid(string make, string model, decimal cost, decimal tax, decimal total)
        {
            var bike = new Bike(make, model, cost);
            _order.AddLine(new Line(bike, 1));

            var expected = string.Format(HtmlReceiptTemplate,make, model, cost, tax, total,CompanyName);
            _order.HtmlReceipt().Should().Be(expected);
        }
    }
}


