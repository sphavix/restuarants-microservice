using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
using Restuarants.Infrastructure.Persistance;

namespace Restuarants.Infrastructure.Repositories
{
    public class DishRepository : IDishRepository
    {
        private readonly RestuarantDbContext _context;

        public DishRepository(RestuarantDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<Guid> CreateDishAsync(Dish dish)
        {
            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();
            return dish.Id;
        }

        public async Task DeleteDishesAsync(IEnumerable<Dish> dishes)
        {
            _context.Dishes.RemoveRange(dishes);
            await _context.SaveChangesAsync();
        }
    }
}
