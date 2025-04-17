using Microsoft.AspNetCore.Http;
using Restuarants.Application.Users.Abstract;
using System.Security.Claims;

namespace Restuarants.Application.Users
{
    public class UserContext(IHttpContextAccessor contextAccessor) : IUserContext
    {
        public CurrentUser? GetCurrentUser()
        {
            var user = contextAccessor?.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("user context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(x => x.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(x => x.Type == ClaimTypes.Role)!.Select(x => x.Value);
            var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
            var dateOfBirthAsString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
            var dateOfBirth = dateOfBirthAsString == null 
                ? (DateOnly?)null 
                : DateOnly.ParseExact(dateOfBirthAsString, "yyyy-MM-dd"); 

            return new CurrentUser(userId, email, roles, dateOfBirthAsString, dateOfBirth);
        }
    }
}
