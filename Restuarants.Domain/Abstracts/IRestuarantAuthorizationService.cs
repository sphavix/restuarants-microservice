using Restuarants.Domain.Constants;
using Restuarants.Domain.Entities;

namespace Restuarants.Domain.Abstracts
{
    public interface IRestuarantAuthorizationService
    {
        bool Authorize(Restuarant restuarant, ResourceOperation operation);
    }
}