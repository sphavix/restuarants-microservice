using MediatR;
using Restuarants.Application.Restuarants.Dtos;

namespace Restuarants.Application.Restuarants.Queries.GetAllRestuarants
{
    public class GetRestuarantsQuery : IRequest<IList<RestuarantDto>>
    {
    }
}
