using Restuarants.Application.Dishes.Dtos;
using Restuarants.Domain.Entities;

namespace Restuarants.Application.Restuarants.Dtos
{
    public class RestuarantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<DishDto> Dishes { get; set; } = [];

    }
}
