
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Users.Abstract;

namespace Restuarants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
        IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly IUserContext _userContext = userContext;

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();

            logger.LogInformation("User: {Email}, date of birth {DoB} - Handling Minimum Age Requirement", currentUser.Email);

            if(currentUser.DateOfBirth == null)
            {
                logger.LogWarning("User date of birth is null");
                context.Fail();
                return Task.CompletedTask;
            }

            //if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge <= DateOnly.FromDateTime(DateOnly.(DateTime.Now))))
            //{
            //    logger.LogInformation("Authorization succeeded");
            //    context.Succeed(requirement);
            //}
            //else
            //{
            //    context.Fail();
            //}

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
