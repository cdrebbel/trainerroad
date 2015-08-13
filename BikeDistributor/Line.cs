namespace BikeDistributor
{
    public class Line
    {
        private ICostCalculator _costCalculator;

        public Line(Bike bike, int quantity) : this(bike, quantity, new CostCalculator()) { }
        public  Line(Bike bike, int quantity, ICostCalculator costCalculator)
        {
            _costCalculator = costCalculator;
            
            Brand = bike.Brand;
            Model = bike.Model;
            Price = bike.Price;
            Quantity = quantity;
            Cost = _costCalculator.CalculateCost(quantity, Price);
        } 
        
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; private set; }
        public decimal Cost { get; private set; }
        public int Quantity { get; private set; }
    }
}
