using Microsoft.AspNetCore.Authorization;

namespace Restuarants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestuarantsRequirement(int minimumRestuarantsCreated) : IAuthorizationRequirement
    {
        public int MinimumRestuarantsCreated { get;  } = minimumRestuarantsCreated;
    }
}
