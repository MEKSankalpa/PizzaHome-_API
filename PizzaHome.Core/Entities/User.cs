using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(257)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(191)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }

        public UserRole Role { get; set; } = UserRole.User; // Ither User or Admin
    }
}
