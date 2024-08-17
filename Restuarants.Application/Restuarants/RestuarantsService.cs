using Microsoft.Extensions.Logging;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants
{
    public class RestuarantsService(IRestuarantsRepository repository, ILogger<RestuarantsService> logger) : IRestuarantsService
    {
        public async Task<IEnumerable<Restuarant>> GetRestuarants()
        {
            logger.LogInformation("Getting all restuarants");
            var restuarants = await repository.GetRestuarantsAsync();
            return restuarants;
        }

        public async Task<Restuarant> GetRestuarant(Guid id)
        {
            logger.LogInformation($"Getting Restuarant with: {id}");
            var restuarant = await repository.GetRestuarantAsync(id);
            return restuarant;
        }
    }
}
