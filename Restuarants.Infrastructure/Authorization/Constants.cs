

namespace Restuarants.Infrastructure.Authorization
{
    public static class PolicyNames
    {
        public const string HasNationality = "HasNationality";
        public const string AtLeast20Yrs = "AtLeast20Yrs";
        public const string CreatedAtLeast2Restuarants = "CreatedAtLeast2Restuarants";
    }

    public static class ApplicationClaimTypes
    {
        public const string Nationality = "Nationality";
        public const string DateOfBirth = "DateOfBirth";
    }
}
