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

            if (price >= 5000)
            {
                if (quantity >= 5)
                {
                    discount = .8m;
                }
            }
            else if (price >= 2000)
            {
                if (quantity >= 10)
                {
                    discount = .8m;
                }
            }
            else if (price >= 1000)
            {
                if (quantity >= 20)
                {
                    discount = .9m;
                }
            }

            return quantity * price * discount;
        }
    }
}
