using MediatR;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishesForRestuarant
{
    public class GetDishesForRestuarantQuery : IRequest<IEnumerable<DishDto>>
    {
        public Guid RestuarentId { get; }

        public GetDishesForRestuarantQuery(Guid restuarantId)
        {
            RestuarentId = restuarantId;
        }
    }
}
