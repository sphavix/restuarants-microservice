using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Entities;

namespace Restuarants.Application.Restuarants
{
    public interface IRestuarantsService
    {
        Task<IEnumerable<RestuarantDto>> GetRestuarants();
        Task<RestuarantDto?> GetRestuarant(Guid id);
    }
}