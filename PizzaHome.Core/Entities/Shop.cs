using System.ComponentModel.DataAnnotations;

namespace PizzaHome.Core.Entities
{
    public class Shop
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string ShopName { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string AddressNo { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Street { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string City { get; set; } = string.Empty;

        [Required]
        [Range(0,1)]
        public ShopStatus Status { get; set; } = ShopStatus.Available;// Getting available or not available
    }
}
