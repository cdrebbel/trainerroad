namespace BikeDistributor
{
    public class Bike
    {
        public Bike(string brand, string model, decimal price)
        {
            Brand = brand;
            Model = model;
            Price = price;
        }

        public string Brand { get; private set; }
        public string Model { get; private set; }
        public decimal Price { get; set; }
    }
}
