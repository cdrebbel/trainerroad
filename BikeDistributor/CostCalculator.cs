using System.Collections.Generic;
using System.Linq;

namespace BikeDistributor
{
    public interface ICostCalculator
    {
        decimal CalculateCost(int quantity, decimal price);
    }

    public class DiscountTier
    {
        public decimal Price;
        public int Quantity;
        public decimal Discount;
    }

    public class CostCalculator : ICostCalculator
    {
        //TODO: Move this out elsewhere so that CostCalculator doesn't care about it.
        private List<DiscountTier> _discountTiers = new List<DiscountTier>()
        {
            new DiscountTier {Price = 5000m, Quantity = 5, Discount = .8m },
            new DiscountTier {Price = 2000m, Quantity = 10, Discount = .8m },
            new DiscountTier {Price = 1000m, Quantity = 20, Discount = .9m }
        };

        public decimal CalculateCost(int quantity, decimal price)
        {
            var discount = 1m;

            foreach (var discountTier in _discountTiers.OrderByDescending(dt => dt.Price))
            {
                if(price >= discountTier.Price && quantity >= discountTier.Quantity)
                {
                    discount = discountTier.Discount;
                    break;
                }
            }
            return quantity * price * discount;
        }
    }
}