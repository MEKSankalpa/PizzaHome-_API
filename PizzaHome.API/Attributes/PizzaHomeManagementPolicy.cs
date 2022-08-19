using Microsoft.AspNetCore.Authorization;
using PizzaHome.Core.Interfaces;
using System.Security.Claims;

namespace PizzaHome.API.Attributes
{
    public class PizzaHomeManagementPolicy : IAuthorizationRequirement
    {

    }

    public class PizzaHomeManagementRequirementHandler : AuthorizationHandler<PizzaHomeManagementPolicy>
    {
        private readonly IUserService _service;
       

        public PizzaHomeManagementRequirementHandler(IUserService service)
        {
            _service = service;
          
        }
        protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, PizzaHomeManagementPolicy requirement)
        {
            var username = context.User.FindFirst(c => c.Type == "UserName").Value;
            var user     = await _service.GetUserByName(username);
            var role     = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;


            if (role == "Admin")
            {
                context.Succeed(requirement);

            }
            else if (role == "User" && user.Email == "aizenit@email.com")
            {
                context.Succeed(requirement);
            } 

            return Task.CompletedTask;

        }
    }
}
