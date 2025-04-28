
using MediatR;

namespace Restuarants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int? Calories { get; set; }
        public int RestuarantId { get; set; }
    }
}
