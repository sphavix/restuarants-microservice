using Microsoft.EntityFrameworkCore;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
using Restuarants.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var restuarants = await _context.Restuarants.ToListAsync();
            return restuarants;
        }

    }
}
