using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Common;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Repositories;

namespace Restuarants.Application.Restuarants.Queries.GetAllRestuarants
{
    public class GetRestuarantsQueryHandler : IRequestHandler<GetRestuarantsQuery, PagedResult<RestuarantDto>>
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

        public async Task<PagedResult<RestuarantDto>> Handle(GetRestuarantsQuery query, CancellationToken cancellationToken)
        {
           
            _logger.LogInformation("Getting all restuarants");

            var (restuarants, totaltCount) = await _restuarantsRepository.GetMatchingRestuarantsAsync(query.SearchPhrase,
                query.PageSize,
                query.PageNumber,
                query.SortBy,
                query.SortOrder);

            //Map using AutoMapper
            var restuarantsDto = _mapper.Map<IList<RestuarantDto>>(restuarants);

            var result = new PagedResult<RestuarantDto>(restuarantsDto, totaltCount, query.PageSize, query.PageNumber);

            return result;
        }
    }
}
