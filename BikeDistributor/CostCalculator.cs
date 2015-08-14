using System.Collections.Generic;
using System.Linq;

namespace BikeDistributor
{
    public interface ICostCalculator
    {
        decimal CalculateCost(int quantity, decimal price);
    }

    public class CostCalculator : ICostCalculator
    {
        public decimal CalculateCost(int quantity, decimal price)
        {
            var discount = 1m;

            foreach (var discountTier in Discounts.Tiers.OrderByDescending(dt => dt.Price))
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