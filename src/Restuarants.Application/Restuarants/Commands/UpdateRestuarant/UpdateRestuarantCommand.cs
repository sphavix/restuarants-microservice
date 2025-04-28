using MediatR;

namespace Restuarants.Application.Restuarants.Commands.UpdateRestuarant
{
    public class UpdateRestuarantCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public bool HasDelivery { get; set; }
    }
}
