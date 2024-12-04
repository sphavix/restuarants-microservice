using MediatR;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishForRestuarant
{
    public class GetDishByIdForRestuarantQuery : IRequest<DishDto>
    {
        public Guid RestuarantId { get; }
        public Guid DishId { get; }

        public GetDishByIdForRestuarantQuery(Guid restuarantId, Guid dishId)
        {
            RestuarantId = restuarantId;
            DishId = dishId;
        }
    }
}
