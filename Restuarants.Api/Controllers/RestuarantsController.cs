using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Restuarants;
using Restuarants.Application.Restuarants.Dtos;

namespace Restuarants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestuarantsController : ControllerBase
    {
        private readonly IRestuarantsService _restuarantsService;
        public RestuarantsController(IRestuarantsService restuarantsService)
        {
            _restuarantsService = restuarantsService ?? throw new ArgumentException(nameof(restuarantsService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var restuarants = await _restuarantsService.GetRestuarants();
            return Ok(restuarants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestuarant([FromRoute] Guid id)
        {
            var restuarant = await _restuarantsService.GetRestuarant(id);
            if (restuarant is null)
                return NotFound();

            return Ok(restuarant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestuarant([FromBody] CreateRestuarantDto createRestuarantDto)
        {
           return Ok(await _restuarantsService.CreateRestuarant(createRestuarantDto));            
        }
    }
}
