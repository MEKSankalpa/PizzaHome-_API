using Microsoft.AspNetCore.Authorization;

namespace PizzaHome.API.Authorization
{
    public class AuthorizeByRoleAttribute : AuthorizeAttribute
    {
        public AuthorizeByRoleAttribute(params string[] roles) {

            Roles = String.Join(",", roles);

        }
    }
}
