using Microsoft.AspNetCore.Identity;

namespace Restuarants.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }

        public List<Restuarant> OwnedRestuarants { get; set; } = [];

    }
}
