using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaHome.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;


        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; } = string.Empty;


        [Required]
        public int Price { get; set; }  


        [Required]
        public int CategoryId { get; set; }




    }
}
