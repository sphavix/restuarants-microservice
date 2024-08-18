using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Queries.GetAllRestuarants
{
    public class GetRestuarantsQueryHandler : IRequestHandler<GetRestuarantsQuery, IList<RestuarantDto>>
    {
        private readonly IRestuarantsRepository _restuarantsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetRestuarantsQueryHandler(IRestuarantsRepository restuarantsRepository, IMapper mapper, ILogger<GetRestuarantsQueryHandler> logger)
        {
            _restuarantsRepository = restuarantsRepository ?? throw new ArgumentNullException(nameof(restuarantsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IList<RestuarantDto>> Handle(GetRestuarantsQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all restuarants");
            var restuarants = await _restuarantsRepository.GetRestuarantsAsync();

            //Map using AutoMapper
            var restuarantsDto = _mapper.Map<IList<RestuarantDto>>(restuarants);

            return restuarantsDto;
        }
    }
}
