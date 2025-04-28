using Restuarants.Domain.Entities;

namespace Restuarants.Domain.Repositories
{
    public interface IDishRepository
    {
        Task<int> CreateDishAsync(Dish dish);
        Task DeleteDishesAsync(IEnumerable<Dish> dishes);
    }
}
