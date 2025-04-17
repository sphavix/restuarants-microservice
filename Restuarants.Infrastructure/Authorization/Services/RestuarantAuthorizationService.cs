using Microsoft.Extensions.Logging;
using Restuarants.Application.Users.Abstract;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Constants;
using Restuarants.Domain.Entities;

namespace Restuarants.Infrastructure.Authorization.Services
{
    public class RestuarantAuthorizationService(
        IUserContext userContext,
        ILogger<RestuarantAuthorizationService> logger) : IRestuarantAuthorizationService
    {
        private readonly IUserContext _userContext = userContext;
        private readonly ILogger<RestuarantAuthorizationService> _logger = logger;

        public bool Authorize(Restuarant restuarant, ResourceOperation operation)
        {
            var user = _userContext.GetCurrentUser();

            _logger.LogInformation("Authoring user {UseerEmail}, to {Operation} restuarant {RestuarantName}", user.Email, operation, restuarant.Id);

            if (operation == ResourceOperation.Read || operation == ResourceOperation.Create)
            {
                _logger.LogInformation("Create/Reade operation authorized");
                return true;
            }

            if (operation == ResourceOperation.Delete && user.IsInRole(UserRole.Admin))
            {
                _logger.LogInformation("Admin User, Delete operation authorized");
                return true;
            }

            if (operation == ResourceOperation.Delete || operation == ResourceOperation.Update && user.Id == restuarant.OwnerId)
            {
                _logger.LogInformation("User is the owner of the restuarant, operation authorized");
                return true;
            }

            return false;
        }
    }
}
