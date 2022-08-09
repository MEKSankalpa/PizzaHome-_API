namespace PizzaHome.Models
{
    public class Shop
    {
        public int Id { get; set; }
        public string ShopName { get; set; } = String.Empty;

        public string ShopLocation { get; set; } = String.Empty;

        public ShopStatus Status { get; set; } // Getting available or not available
    }
}
