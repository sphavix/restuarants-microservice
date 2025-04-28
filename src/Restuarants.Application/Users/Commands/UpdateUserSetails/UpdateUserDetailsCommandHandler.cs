using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Entities;
using Restuarants.Application.Users.Abstract;

namespace Restuarants.Application.Users.Commands.UpdateUserSetails
{
    public class UpdateUserDetailsCommandHandler(ILogger<UpdateUserDetailsCommandHandler> _logger,
        IUserContext _userContext,
        IUserStore<ApplicationUser> _userStore) : IRequestHandler<UpdateUserDetailsCommand>
    {
        public async Task Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var user = _userContext.GetCurrentUser();

            _logger.LogInformation("Updating user: {userId}, with {@Request} details.", user!.Id, request);

            var dbUser = await _userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
            {
                throw new Exception("User not found!");
            }

            dbUser.DateOfBirth = request.DateOfBirth;
            dbUser.Nationality = request.Nationality;

            await _userStore.UpdateAsync(dbUser, cancellationToken);
        }
    }
}
