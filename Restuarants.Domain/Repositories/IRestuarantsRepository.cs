using Restuarants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Domain.Repositories
{
    public interface IRestuarantsRepository
    {
        Task<IEnumerable<Restuarant>> GetRestuarantsAsync();
        Task<Restuarant> GetRestuarantAsync(Guid id);
        Task<Restuarant> CreateRestuarantAsync(Restuarant restuarant);
        Task<bool> UpdateRestuarantAsync(Restuarant restuarant);
        Task<bool> DeleteRestuarantAsync(Guid id);
    }
}
