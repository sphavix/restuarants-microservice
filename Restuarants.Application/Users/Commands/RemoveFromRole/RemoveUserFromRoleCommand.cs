

using MediatR;

namespace Restuarants.Application.Users.Commands.RemoveFromRole
{
    public class RemoveUserFromRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = default!;
        public string RoleName { get; set; } = default!;
    }
}
