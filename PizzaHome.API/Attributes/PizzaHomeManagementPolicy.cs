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
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PizzaHomeManagementPolicy requirement)
        {
            var username = context.User?.FindFirst(c => c.Type == "UserName")?.Value;
            if (username is null)  throw new UnauthorizedAccessException("Unauthorized!");
            var role = context.User?.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;


            if (role == "Admin")
            {
                context.Succeed(requirement);
            }
            else if (role == "User" && username == "AizenitUser")
            {
                context.Succeed(requirement);
            } 

            return Task.CompletedTask;

        }
    }
}
