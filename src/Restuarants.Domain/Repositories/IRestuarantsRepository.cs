using Restuarants.Domain.Constants;
using Restuarants.Domain.Entities;

namespace Restuarants.Domain.Repositories
{
    public interface IRestuarantsRepository
    {
        Task<IEnumerable<Restuarant>> GetRestuarantsAsync();
        Task<(IEnumerable<Restuarant>, int)> GetMatchingRestuarantsAsync(string? searchPhrase, int pageSize, int pageNumber, string? sortBy, SortDirection sortOrder);
        Task<Restuarant> GetRestuarantAsync(int id);
        Task<int> CreateRestuarantAsync(Restuarant restuarant);
        //Task UpdateRestuarantAsync(Restuarant restuarant);
        Task SaveChanges();
        Task<bool> DeleteRestuarantAsync(int id);
    }
}
