using Microsoft.EntityFrameworkCore;
using Restuarants.Domain.Constants;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
using Restuarants.Infrastructure.Persistance;
using System.Linq.Expressions;

namespace Restuarants.Infrastructure.Repositories
{
    public class RestuarantsRepository : IRestuarantsRepository
    {
        private readonly RestuarantDbContext _context;

        public RestuarantsRepository(RestuarantDbContext context) 
        { 
            _context = context;
        }

        public async Task<IEnumerable<Restuarant>> GetRestuarantsAsync()
        {
            var restuarants = await _context.Restuarants.Include(x => x.Dishes).ToListAsync();
            return restuarants;
        }

        public async Task<(IEnumerable<Restuarant>, int)> GetMatchingRestuarantsAsync(string? searchPhrase, 
            int pageSize, 
            int pageNumber, 
            string? sortBy, 
            SortDirection sortOrder) // Tuple returns 2 element
        {
            var searchPhraseLower = searchPhrase?.ToLower();

            var baseQuery = _context.Restuarants
                .Where(x => searchPhraseLower == null || (x.Name.ToLower().Contains(searchPhraseLower))
                                                        || x.Description.ToLower().Contains(searchPhraseLower));

            var totalCount = await baseQuery.CountAsync();

            if(sortBy != null)
            {
                // create dictionary for our sortby & sortOrder
                var columnsSelector = new Dictionary<string, Expression<Func<Restuarant, object>>>
                {
                    { nameof(Restuarant.Name), x => x.Name },
                    { nameof(Restuarant.Description), x => x.Description },
                    { nameof(Restuarant.Category), x => x.Category },
                };

                var selectedColumn = columnsSelector[sortBy];
                baseQuery = sortOrder == SortDirection.Ascending 
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restuarants = await baseQuery
                .Skip(pageSize * (pageNumber -1))
                .Take(pageSize)
                .ToListAsync();

            return (restuarants, totalCount);
        }

        public async Task<Restuarant> GetRestuarantAsync(int id)
        {
            var restuarant = await _context.Restuarants.Where(x => x.Id == id)
                .Include(x => x.Dishes).FirstOrDefaultAsync().ConfigureAwait(true);
            return restuarant;
        }

        public async Task<int> CreateRestuarantAsync(Restuarant restuarant)
        {
            _context.Restuarants.Add(restuarant);
            await _context.SaveChangesAsync();

            return restuarant.Id;
        }

        //public async Task UpdateRestuarantAsync(Restuarant restuarant)
        //{
        //   _context.Restuarants.Update(restuarant);
        //  await _context.SaveChangesAsync().ConfigureAwait(true) > 0;
        //}

        public async Task<bool> DeleteRestuarantAsync(int id)
        {
            var restuarant = await _context.Restuarants.FindAsync(id);
            _context.Restuarants.Remove(restuarant);
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return true;
        }

        public Task SaveChanges() => _context.SaveChangesAsync();

    }
}
