using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Users.Commands.AssignRole;
using Restuarants.Application.Users.Commands.RemoveFromRole;
using Restuarants.Application.Users.Commands.UpdateUserSetails;
using Restuarants.Domain.Constants;

namespace Restuarants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController(IMediator _mediator) : ControllerBase
    {
        [HttpPatch("updateUserDetails")]
        [Authorize]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserDetailsCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPost("assignToRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> AssignUserRoles(AssignUserRoleCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("removeFromRole")]
        [Authorize(Roles = UserRole.Admin)]
        public async Task<IActionResult> RemoverUserFromRole(RemoveUserFromRoleCommand command)
        {
            await _mediator.Send(command);

            return NoContent();
        }
    }
}
