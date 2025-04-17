using Restuarants.Domain.Constants;
using Restuarants.Domain.Entities;

namespace Restuarants.Domain.Repositories
{
    public interface IRestuarantsRepository
    {
        Task<IEnumerable<Restuarant>> GetRestuarantsAsync();
        Task<(IEnumerable<Restuarant>, int)> GetMatchingRestuarantsAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortOrder);
        Task<Restuarant> GetRestuarantAsync(Guid id);
        Task<Restuarant> CreateRestuarantAsync(Restuarant restuarant);
        //Task UpdateRestuarantAsync(Restuarant restuarant);
        Task SaveChanges();
        Task<bool> DeleteRestuarantAsync(Guid id);
    }
}
