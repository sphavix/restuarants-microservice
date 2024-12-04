
using MediatR;

namespace Restuarants.Application.Dishes.Commands.DeleteDishes
{
    public class DeleteDishesForRestuarantCommand : IRequest
    {
        public Guid RestuarantId { get; }

        public DeleteDishesForRestuarantCommand(Guid restuarantId)
        {
            RestuarantId = restuarantId;
        }
    }
}
