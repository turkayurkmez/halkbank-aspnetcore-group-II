using Microsoft.AspNetCore.Authorization;

namespace ECommerce.API.AuthPolicies
{
    public record MinAgeRequirement(int Age) : IAuthorizationRequirement;

    public class MinAgeHandler : AuthorizationHandler<MinAgeRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
        {
            var claim = context.User.FindFirst("birthdate").Value;
            var birthDate = DateOnly.ParseExact(claim, "yyyy-MM-dd");
            var age = DateTime.Now.Year - birthDate.Year;
            if (age>= requirement.Age)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
         }
    }

}
