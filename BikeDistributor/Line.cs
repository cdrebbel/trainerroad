namespace BikeDistributor
{
    public class Line
    {
        public Line(Bike bike, int quantity)
        {
            Brand = bike.Brand;
            Model = bike.Model;
            Price = bike.Price;
            Quantity = quantity;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; private set; }
        public int Quantity { get; private set; }
    }
}
