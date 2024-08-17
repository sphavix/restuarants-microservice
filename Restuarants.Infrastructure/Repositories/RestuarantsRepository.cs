using Microsoft.EntityFrameworkCore;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
using Restuarants.Infrastructure.Persistance;

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

        public async Task<Restuarant> GetRestuarantAsync(Guid id)
        {
            var restuarant = await _context.Restuarants.Where(x => x.Id == id)
                .Include(x => x.Dishes).FirstOrDefaultAsync().ConfigureAwait(true);
            return restuarant;
        }

    }
}
