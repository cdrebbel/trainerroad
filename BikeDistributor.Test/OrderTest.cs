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
        private Bike _bike;

        private string HtmlReceiptTemplate = @"<html><body><h1>Order Receipt for {5}</h1><ul><li>1 x {0} {1} = {2:C}</li></ul><h3>Sub-Total: {2:C}</h3><h3>Tax: {3:C}</h3><h2>Total: {4:C}</h2></body></html>";
        private string ReceiptTemplate = @"Order Receipt for {5}
	1 x {0} {1} = {2:C}
Sub-Total: {2:C}
Tax: {3:C}
Total: {4:C}";

        [SetUp]
        public void Setup()
        {;
            _order = new Order(CompanyName, TaxRate);
        }

        [TestCase("Specialized","S-Works Venge Dura-Ace",5000.00, 362.50)]
        [TestCase("Specialized","Venge Elite",2000.00,145.00)]
        [TestCase("Giant","Defy 1",1000.00, 72.50)]
        public void ReceiptIsValid(string make, string model, decimal cost, decimal tax)
        {
            GivenBike(make, model, cost);
            WhenBikeAddedToOrder();
            ThenReceiptIsValid(_order.Receipt(), ReceiptTemplate, make, model, cost, tax);
        }

        [TestCase("Specialized", "S-Works Venge Dura-Ace", 5000.00, 362.50)]
        [TestCase("Specialized", "Venge Elite", 2000.00, 145.00)]
        [TestCase("Giant", "Defy 1", 1000.00, 72.50)]
        public void HtmlReceiptIsValid(string make, string model, decimal cost, decimal tax)
        {
            GivenBike(make, model, cost);
            WhenBikeAddedToOrder();
            ThenReceiptIsValid(_order.HtmlReceipt(), HtmlReceiptTemplate, make, model, cost, tax);
        }

        private void GivenBike(string make, string model, decimal cost)
        {
            _bike = new Bike(make, model, cost);
        }
        private void WhenBikeAddedToOrder()
        {
            _order.AddLine(new Line(_bike, 1));
        }

        //TODO: Figure out a better way to test that the format is good and don't worry so much about the specifics (make,model,cost,tax)
        private void ThenReceiptIsValid(string receipt, string receiptTemplate, string make, string model, decimal cost, decimal tax)
        {
            var total = cost + tax;
            var expected = string.Format(receiptTemplate, make, model, cost, tax, total, CompanyName);
            receipt.Should().Be(expected);
        }
    }
}