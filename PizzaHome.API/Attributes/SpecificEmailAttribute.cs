using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PizzaHome.Core.Interfaces;

namespace PizzaHome.API.Attributes
{
    public class SpecificEmailAttribute : Attribute, IAuthorizationFilter
    {
        public string UserName { get; set; }

        public SpecificEmailAttribute (string username)
        {
            UserName = username;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userName = context.HttpContext.User.Claims.First(c => c.Type == "UserName").Value;
            Console.WriteLine(userName);
            if (userName != UserName ) {
                context.Result = new ForbidResult();
            }
        }
    }
}
