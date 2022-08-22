using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface IAuthService
    {
    
        public string GenerateAccessToken(string email, string role);
        public string GenerateRefreshToken(string email, string role);
        public object RegenerateTokens(string token);
    }
}
