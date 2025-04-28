
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Entities;

namespace Restuarants.Application.Users.Commands.RemoveFromRole
{
    public class RemoveUserFromRoleCommandHandler(ILogger<RemoveUserFromRoleCommandHandler> logger,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole> _roleManager) : IRequestHandler<RemoveUserFromRoleCommand>
    {
        public async Task Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Removing user from role: {@Request}", request);

            var user = await _userManager.FindByEmailAsync(request.UserEmail);

            if(user == null)
            {
                throw new Exception("User cannot be found");
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if(role == null)
            {
                throw new Exception("Role does not exist");
            }

            await _userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
