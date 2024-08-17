using AutoMapper;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants
{
    public class RestuarantsService(IRestuarantsRepository repository, 
        ILogger<RestuarantsService> logger,
        IMapper mapper) : IRestuarantsService
    {
        public async Task<IEnumerable<RestuarantDto>> GetRestuarants()
        {
            logger.LogInformation("Getting all restuarants");
            var restuarants = await repository.GetRestuarantsAsync();

            //Map the entity from Dto manually
            //var restuarantsDto = restuarants.Select(RestuarantDto.FromEntity);

            //Map using AutoMapper
            var restuarantsDto = mapper.Map<IEnumerable<RestuarantDto>>(restuarants);

            return restuarantsDto!;
        }

        public async Task<RestuarantDto?> GetRestuarant(Guid id)
        {
            logger.LogInformation($"Getting Restuarant with: {id}");
            var restuarant = await repository.GetRestuarantAsync(id);

            // Map entity from Dto manually
            //var restuarantDto = RestuarantDto.FromEntity(restuarant);

            // Map  using AutoMapper
            var restuarantDto = mapper.Map<RestuarantDto?>(restuarant);

            return restuarantDto;
        }

        public async Task<CreateRestuarantDto> CreateRestuarant(CreateRestuarantDto createRestuarantDto)
        {
            logger.LogInformation("Creating a new restuarant");

            // Map entity to Dto
            var restuarant = mapper.Map<Restuarant>(createRestuarantDto);

            await repository.CreateRestuarantAsync(restuarant);

            // Map Dto back to entity
            var response = mapper.Map<CreateRestuarantDto>(restuarant);

            return response;
        }
    }
}
