using Microsoft.Extensions.Logging;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants
{
    public class RestuarantsService(IRestuarantsRepository repository, ILogger<RestuarantsService> logger) : IRestuarantsService
    {
        public async Task<IEnumerable<RestuarantDto>> GetRestuarants()
        {
            logger.LogInformation("Getting all restuarants");
            var restuarants = await repository.GetRestuarantsAsync();

            //Map the entity from Dto
            var restuarantsDto = restuarants.Select(RestuarantDto.FromEntity);
            return restuarantsDto!;
        }

        public async Task<RestuarantDto?> GetRestuarant(Guid id)
        {
            logger.LogInformation($"Getting Restuarant with: {id}");
            var restuarant = await repository.GetRestuarantAsync(id);

            var restuarantDto = RestuarantDto.FromEntity(restuarant);
            return restuarantDto;
        }
    }
}
