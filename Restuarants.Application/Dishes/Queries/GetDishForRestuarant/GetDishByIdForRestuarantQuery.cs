using MediatR;
using Restuarants.Application.Dishes.Dtos;

namespace Restuarants.Application.Dishes.Queries.GetDishForRestuarant
{
    public class GetDishByIdForRestuarantQuery : IRequest<DishDto>
    {
        public int RestuarantId { get; }
        public int DishId { get; }

        public GetDishByIdForRestuarantQuery(int restuarantId, int dishId)
        {
            RestuarantId = restuarantId;
            DishId = dishId;
        }
    }
}
