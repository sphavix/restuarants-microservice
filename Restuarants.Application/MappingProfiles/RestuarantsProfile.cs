using AutoMapper;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restuarants.Application.MappingProfiles
{
    public class RestuarantsProfile : Profile
    {
        public RestuarantsProfile()
        {
            CreateMap<Restuarant, RestuarantDto>()
                .ForMember(d => d.City, option =>
                    option.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(d => d.Street, option =>
                    option.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(d => d.PostalCode, option =>
                    option.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(d => d.Dishes, option => option.MapFrom(src => src.Dishes)).ReverseMap();

            CreateMap<CreateRestuarantDto, Restuarant>()
                .ForMember(a => a.Address, opt => opt.MapFrom(
                    src => new Address
                    {
                        City = src.City,
                        Street = src.Street,
                        PostalCode = src.PostalCode
                    }))
                .ReverseMap();
        }
    }
}
