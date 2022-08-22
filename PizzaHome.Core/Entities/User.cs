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
        [StringLength(50, MinimumLength = 3)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; } = string.Empty ;

        [Required]
        [StringLength(10, MinimumLength = 5)]
        public string Password { get; set; } = string.Empty;

       
        public UserRole Role { get; set; } = UserRole.User; // Ither User or Admin
    }
}
