using System.ComponentModel.DataAnnotations;

namespace PizzaHome.Core.Entities
{
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string ShopName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string AddressNo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string City { get; set; } = string.Empty;

        public ShopStatus Status { get; set; } = ShopStatus.Available;// Getting available or not available
    }
}
