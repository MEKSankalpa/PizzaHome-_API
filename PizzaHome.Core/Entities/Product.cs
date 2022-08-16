using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PizzaHome.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(37)]
        public string Name { get; set; }


        [Required]
        [MaxLength(250)]
        public string Description { get; set; }


        [Required]
        public int Price { get; set; }


        [Required]
        public int CategoryId { get; set; }




    }
}
