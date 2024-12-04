using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Dishes.Dtos;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Dishes.Queries.GetDishesForRestuarant
{
    public class GetDishesForRestuarantQueryHandler : IRequestHandler<GetDishesForRestuarantQuery, IEnumerable<DishDto>>
    {
        private readonly ILogger<GetDishesForRestuarantQueryHandler> _logger;
        private readonly IRestuarantsRepository _restuarantRepository;
        private readonly IMapper _mapper;

        public GetDishesForRestuarantQueryHandler(ILogger<GetDishesForRestuarantQueryHandler> logger, IRestuarantsRepository restuarantRepository,
            IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _restuarantRepository = restuarantRepository ?? throw new ArgumentNullException(nameof(restuarantRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestuarantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving dishes for restuarant with id: {RestuarantId}", request.RestuarentId);

            var restuarant = await _restuarantRepository.GetRestuarantAsync(request.RestuarentId);

            if(restuarant is null)
            {
                throw new ApplicationException("Unable to retrieve dishes for the restuarant");
            }

            // map dto to domain entity
            var response = _mapper.Map<IEnumerable<DishDto>>(restuarant.Dishes);

            return response;
        }
    }
}
