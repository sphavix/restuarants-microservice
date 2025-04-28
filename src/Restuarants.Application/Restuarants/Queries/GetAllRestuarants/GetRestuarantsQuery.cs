using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restuarants.Application.Common;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Constants;

namespace Restuarants.Application.Restuarants.Queries.GetAllRestuarants
{
    public class GetRestuarantsQuery : IRequest<PagedResult<RestuarantDto>>
    {
        public string? SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }
        public SortDirection SortOrder { get; set; }
    }
}
