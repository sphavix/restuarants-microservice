﻿using Restuarants.Domain.Entities;

namespace Restuarants.Application.Restuarants
{
    public interface IRestuarantsService
    {
        Task<IEnumerable<Restuarant>> GetRestuarants();
    }
}