using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Queries.GetRestuarant
{
    public class GetRestuarantByIdQueryHandler : IRequestHandler<GetRestuarantByIdQuery, RestuarantDto?>
    {
        private readonly IRestuarantsRepository _restuarantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetRestuarantByIdQueryHandler(IRestuarantsRepository restuarantRepository, IMapper mapper, ILogger<GetRestuarantByIdQueryHandler> logger)
        {
            _restuarantRepository = restuarantRepository ?? throw new ArgumentNullException(nameof(restuarantRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<RestuarantDto?> Handle(GetRestuarantByIdQuery query, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting restuarant by {query.Id}");

            var restuarant = await _restuarantRepository.GetRestuarantAsync(query.Id);

            var response = _mapper.Map<RestuarantDto>(restuarant);

            return response;
        }
    }
}
