using NUnit.Framework;
using FluentAssertions;

namespace BikeDistributor.Test
{
    [TestFixture]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike("Giant", "Defy 1", Bike.OneThousand);
        private readonly static Bike Elite = new Bike("Specialized", "Venge Elite", Bike.TwoThousand);
        private readonly static Bike DuraAce = new Bike("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand);

        [TestCase("Specialized","S-Works Venge Dura-Ace",Bike.FiveThousand, 362.50, 5362.50)]
        [TestCase("Specialized","Venge Elite",Bike.TwoThousand,145.00,2145.00)]
        [TestCase("Giant","Defy 1",Bike.OneThousand, 72.50, 1072.5)]
        public void ReceiptIsValid(string make, string model, int cost, decimal tax, decimal total)
        {
            var bike = new Bike(make, model, cost);
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(bike, 1));
            var expected = string.Format(@"Order Receipt for Anywhere Bike Shop
	1 x {0} {1} = {2:C}
Sub-Total: {2:C}
Tax: {3:C}
Total: {4:C}", make, model, cost,tax,total);

            order.Receipt().Should().Be(expected);
        }

        [TestCase("Specialized", "S-Works Venge Dura-Ace", Bike.FiveThousand, 362.50, 5362.50)]
        [TestCase("Specialized", "Venge Elite", Bike.TwoThousand, 145.00, 2145.00)]
        [TestCase("Giant", "Defy 1", Bike.OneThousand, 72.50, 1072.5)]
        public void HtmlReceiptIsValid(string make, string model, int cost, decimal tax, decimal total)
        {
            var bike = new Bike(make, model, cost);
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(bike, 1));
            var expected = string.Format(@"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x {0} {1} = {2:C}</li></ul><h3>Sub-Total: {2:C}</h3><h3>Tax: {3:C}</h3><h2>Total: {4:C}</h2></body></html>",
                 make, model, cost, tax, total);
            order.HtmlReceipt().Should().Be(expected);
        }}
}


