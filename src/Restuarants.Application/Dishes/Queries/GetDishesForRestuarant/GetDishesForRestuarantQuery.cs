using MediatR;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishesForRestuarant
{
    public class GetDishesForRestuarantQuery : IRequest<IEnumerable<DishDto>>
    {
        public int RestuarentId { get; }

        public GetDishesForRestuarantQuery(int restuarantId)
        {
            RestuarentId = restuarantId;
        }
    }
}
