using Restuarants.Domain.Entities;

namespace Restuarants.Domain.Repositories
{
    public interface IDishRepository
    {
        Task<Guid> CreateDishAsync(Dish dish);
        Task DeleteDishesAsync(IEnumerable<Dish> dishes);
    }
}
