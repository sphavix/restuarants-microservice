﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Dishes.Commands.CreateDish;
using Restuarants.Application.Dishes.Commands.DeleteDishes;
using Restuarants.Application.Dishes.Dtos;
using Restuarants.Application.Dishes.Queries.GetDishesForRestuarant;
using Restuarants.Application.Dishes.Queries.GetDishForRestuarant;
using Restuarants.Infrastructure.Authorization;

namespace Restuarants.Api.Controllers
{
    [Route("api/restuarant/{restuarantId}/dishes")]
    [ApiController]
    public class DishesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DishesController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        //[Authorize(Policy = PolicyNames.AtLeast20Yrs)]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetDishesForRestuarant([FromRoute] int restuarantId)
        {
            var dishes = new GetDishesForRestuarantQuery(restuarantId);

            var results = await _mediator.Send(dishes);

            return Ok(results);
        }

        [HttpGet("{dishId:guid}")]
        public async Task<ActionResult<DishDto>> GetDishByIdForRestuarant([FromRoute] int restuarantId, [FromRoute] int dishId)
        {
            var dishes = new GetDishByIdForRestuarantQuery(restuarantId, dishId);
            
            var results = await _mediator.Send(dishes);

            return Ok(results);
        }



        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restuarantId, CreateDishCommand command)
        {
            command.RestuarantId = restuarantId;

            var dishId = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetDishByIdForRestuarant), new { restuarantId, dishId }, null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesForRestuarant([FromRoute] int restuarantId)
        {
            var dishes = new DeleteDishesForRestuarantCommand(restuarantId);
            await _mediator.Send(dishes);
            return NoContent();
        }
    }
}
