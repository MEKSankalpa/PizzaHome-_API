using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaHome.Models
{
    public class Category
    { 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = String.Empty;

        [Required]
        public string Description { get; set; } = String.Empty;
   
        public virtual  List<Product>? Products { get; set; }
    }
}
