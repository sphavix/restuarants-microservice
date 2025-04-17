using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restuarants.Domain.Entities;
using System.Security.Claims;

namespace Restuarants.Infrastructure.Authorization
{
    public class ApplicationUserClaimPrincipalFactory(
        UserManager<ApplicationUser> userManager, 
        RoleManager<IdentityRole> roleManager, 
        IOptions<IdentityOptions> options) : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>(userManager, roleManager, options)
    {
        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var id = await GenerateClaimsAsync(user);

            if(user.Nationality != null)
            {
                id.AddClaim(new Claim(ApplicationClaimTypes.Nationality, user.Nationality));
            }

            if(user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(ApplicationClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyy-MM-dd")));
            }

            return new ClaimsPrincipal(id);
        }
    }
}
