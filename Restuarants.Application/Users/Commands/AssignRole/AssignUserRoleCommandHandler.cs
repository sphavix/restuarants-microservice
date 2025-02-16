
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restuarants.Domain.Entities;

namespace Restuarants.Application.Users.Commands.AssignRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger,
        UserManager<ApplicationUser> _userManager,
        RoleManager<IdentityRole>_roleManager) : IRequestHandler<AssignUserRoleCommand>
    {
        public async Task Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning user role: {@Request}", request);

            var user = await _userManager.FindByEmailAsync(request.UserEmail);
            
            if(user == null)
            {
                throw new Exception("User not fount");
            }

            var role = await _roleManager.FindByNameAsync(request.RoleName);

            if(role == null)
            {
                throw new Exception("Role does not exist");
            }

            await _userManager.AddToRoleAsync(user, role.Name!);


        }
    }
}
