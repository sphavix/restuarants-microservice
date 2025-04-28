using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Restuarants.Commands.CreateRestuarant;
using Restuarants.Application.Restuarants.Commands.DeleteRestuarant;
using Restuarants.Application.Restuarants.Commands.UpdateRestuarant;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Application.Restuarants.Queries.GetAllRestuarants;
using Restuarants.Application.Restuarants.Queries.GetRestuarant;
using Restuarants.Domain.Constants;
using Restuarants.Infrastructure.Authorization;

namespace Restuarants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RestuarantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestuarantsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [AllowAnonymous]
        //[Authorize(Policy = PolicyNames.CreatedAtLeast2Restuarants)]
        public async Task<ActionResult<IEnumerable<RestuarantDto>>> GetAll([FromQuery] GetRestuarantsQuery query)
        {
            var restuarants = await _mediator.Send(query);
            //var restuarants = new GetRestuarantsQuery(query);
            //var results = await _mediator.Send(restuarants);
            return Ok(restuarants);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
        public async Task<IActionResult> GetRestuarant([FromRoute] int id)
        {
            var restuarant = new GetRestuarantByIdQuery(id);

            var results = await _mediator.Send(restuarant);

            return Ok(results);
        }

        [HttpPost]
        [Authorize(Roles = UserRole.Owner)]
        public async Task<ActionResult<RestuarantDto>> CreateRestuarant([FromBody] CreateRestuarantCommand command)
        {
            int response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRestuarant), new { response }, response);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestuarant([FromRoute]int id, UpdateRestuarantCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);
           return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestuarant(int id)
        {
            var isDeleted = await _mediator.Send(new DeleteRestuarantCommand(id));
            if (isDeleted)
            {
                return NoContent();
            }

            return NoContent();
        }
    }
}
