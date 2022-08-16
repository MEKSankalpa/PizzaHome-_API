using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaHome.Core.Interfaces
{
    public interface IAuthService
    {
    
        public string GenerateAccessToken(string name);
        public string GenerateRefreshToken(string name);
        public object RegenerateTokens(string token);
    }
}
