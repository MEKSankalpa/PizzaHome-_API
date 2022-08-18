using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 3)]
        public string Name { get; set; } = String.Empty;

        [Required]
        [StringLength(250, MinimumLength = 10)]
        public string Description { get; set; } = String.Empty;

       
    }
}
