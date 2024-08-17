using AutoMapper;
using Restuarants.Application.Dishes.Dtos;
using Restuarants.Domain.Entities;

namespace Restuarants.Application.MappingProfiles
{
    public class DishesProfile : Profile
    {
        public DishesProfile()
        {
            CreateMap<Dish, DishDto>().ReverseMap();
        }
    }
}
