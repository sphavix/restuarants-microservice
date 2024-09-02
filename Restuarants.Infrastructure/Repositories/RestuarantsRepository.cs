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

        public async Task<Restuarant> CreateRestuarantAsync(Restuarant restuarant)
        {
            var result = await _context.Restuarants.AddAsync(restuarant);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            return result.Entity;
        }

        public async Task<bool> UpdateRestuarantAsync(Restuarant restuarant)
        {
           _context.Restuarants.Update(restuarant);
            return await _context.SaveChangesAsync().ConfigureAwait(true) > 0;
        }

        public async Task<bool> DeleteRestuarantAsync(Guid id)
        {
            var restuarant = await _context.Restuarants.FindAsync(id);
            _context.Restuarants.Remove(restuarant);
            await _context.SaveChangesAsync().ConfigureAwait(true);
            return true;
        }

    }
}
