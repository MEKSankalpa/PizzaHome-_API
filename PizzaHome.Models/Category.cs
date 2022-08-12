using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaHome.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [MaxLength(250)]
        public string Description { get; set; } = String.Empty;
   
        public   List<Product> Products { get; set; } = new List<Product>();
    }
}
