using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Dishes.Dtos;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Queries.GetDishForRestuarant
{
    public class GetDishByIdForRestuarantQueryHandler : IRequestHandler<GetDishByIdForRestuarantQuery, DishDto>
    {
        private readonly ILogger<GetDishByIdForRestuarantQuery> _logger;
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly IMapper _mapper;

        public GetDishByIdForRestuarantQueryHandler(ILogger<GetDishByIdForRestuarantQuery> logger, IRestuarantsRepository restuarantsRepository,
            IDishRepository dishRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<DishDto> Handle(GetDishByIdForRestuarantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retreiving dish: {dishId} for a restuarant with I: {RestuarantId}", request.DishId, request.RestuarantId);

            var restuarant = await _restuarantsRepository.GetRestuarantAsync(request.RestuarantId);

            if(restuarant is null)
            {
                throw new ApplicationException("There was an error while requesting dishes for this restuarant.");
            }

            // get dish for the restuarant
            var dish = restuarant.Dishes.FirstOrDefault(x => x.Id == request.DishId);

            if (dish is null)
            {
                throw new ApplicationException("The requested dish cannot be found for the specified restuarant");
            }

            // map dto to entity
            var response = _mapper.Map<DishDto>(dish);

            return response;
        }
    }
}
