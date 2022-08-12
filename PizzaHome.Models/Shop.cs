using System.ComponentModel.DataAnnotations;

namespace PizzaHome.Models
{
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShopName { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string AddressNo { get; set; } = String.Empty;

        [Required]
        [MaxLength(100)]
        public string Street { get; set; } = String.Empty;

        [Required]
        [MaxLength(10)]
        public string City { get; set; } = String.Empty;

        public ShopStatus Status { get; set; } = ShopStatus.Available;// Getting available or not available
    }
}
