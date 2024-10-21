using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Restuarants.Commands.CreateRestuarant;
using Restuarants.Application.Restuarants.Commands.DeleteRestuarant;
using Restuarants.Application.Restuarants.Commands.UpdateRestuarant;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Application.Restuarants.Queries.GetAllRestuarants;
using Restuarants.Application.Restuarants.Queries.GetRestuarant;

namespace Restuarants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestuarantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestuarantsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restuarants = new GetRestuarantsQuery();
            var results = await _mediator.Send(restuarants);
            return Ok(results);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestuarant([FromRoute] Guid id)
        {
            var restuarant = new GetRestuarantByIdQuery(id);

            var results = await _mediator.Send(restuarant);

            return Ok(results);
        }

        [HttpPost]
        public async Task<ActionResult<RestuarantDto>> CreateRestuarant([FromBody] CreateRestuarantCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRestuarant([FromBody] UpdateRestuarantCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestuarant(Guid id)
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
