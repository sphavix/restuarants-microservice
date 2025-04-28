
using MediatR;

namespace Restuarants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestuarantCommand : IRequest
    {
        public int RestuarantId { get; }

        public DeleteDishesForRestuarantCommand(int restuarantId)
        {
            RestuarantId = restuarantId;
        }
    }
}
