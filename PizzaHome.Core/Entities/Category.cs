using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaHome.Core.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
