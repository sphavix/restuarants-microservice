using Microsoft.AspNetCore.Authorization;
using Restuarants.Application.Users.Abstract;
using Restuarants.Domain.Repositories;

namespace Restuarants.Infrastructure.Authorization.Requirements
{
    internal class CreatedMultipleRestuarantsRequirementHandler(
        IRestuarantsRepository restuarantsRepository,
        IUserContext userContext) : AuthorizationHandler<CreatedMultipleRestuarantsRequirement>
    {
        private readonly IRestuarantsRepository _restuarantsRepository = restuarantsRepository;
        private readonly IUserContext _userContext = userContext;
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestuarantsRequirement requirement)
        {
            // Get current loggedin user
            var currentuser = _userContext.GetCurrentUser();

            // Get Restuarants
            var restuarants = await _restuarantsRepository.GetRestuarantsAsync();

            // Get restuarants created by a user
            var userRestuarantsCreated = restuarants.Count(x => x.OwnerId == currentuser!.Id);

            if(userRestuarantsCreated >= requirement.MinimumRestuarantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
