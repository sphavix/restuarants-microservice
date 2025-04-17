using Microsoft.AspNetCore.Authorization;

namespace Restuarants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
    {
        public int MinimumAge { get; } = minimumAge;
    }
}
