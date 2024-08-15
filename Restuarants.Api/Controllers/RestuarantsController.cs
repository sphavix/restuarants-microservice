using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restuarants.Application.Restuarants;

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
    }
}
