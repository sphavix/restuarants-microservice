using AutoMapper;
using FluentAssertions;
using Restuarants.Application.Restuarants.Commands.CreateRestuarant;
using Restuarants.Application.Restuarants.Commands.UpdateRestuarant;
using Restuarants.Application.Restuarants.Dtos;
using Restuarants.Domain.Entities;
using Xunit;

namespace Restuarants.Application.MappingProfiles.Tests
{
    public class RestuarantsProfileTests
    {
        private readonly IMapper _mapper;
        public RestuarantsProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestuarantsProfile>();
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact()]
        public void CreateMap_ForRestuarantToRestuarantDto_MapsCorrectly()
        {
            // arrange
            var restuarant = new Restuarant
            {
                Id = 1,
                Name = "Test Restuarant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@gmail.com",
                ContactNumber = "1234567890",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12345"
                }
            };

            // act
            var restuarantDto = _mapper.Map<RestuarantDto>(restuarant);

            // assert
            restuarantDto.Should().NotBeNull();
            restuarantDto.Id.Should().Be(restuarant.Id);
            restuarantDto.Name.Should().Be(restuarant.Name);
            restuarantDto.Description.Should().Be(restuarant.Description);
            restuarantDto.Category.Should().Be(restuarant.Category);
            restuarantDto.HasDelivery.Should().Be(restuarant.HasDelivery);
            restuarantDto.City.Should().Be(restuarant.Address.City);
            restuarantDto.Street.Should().Be(restuarant.Address.Street);
            restuarantDto.PostalCode.Should().Be(restuarant.Address.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForCreateRestuarantCommandToRestuarant_MapsCorrectly()
        {
            // arrange
            var command = new CreateRestuarantCommand
            {
                Name = "Test Restuarant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@gmail.com",
                ContactNumber = "1234567890",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            };

            // act
            var restuarant = _mapper.Map<Restuarant>(command);

            // assert
            restuarant.Should().NotBeNull();
            restuarant.Name.Should().Be(command.Name);
            restuarant.Description.Should().Be(command.Description);
            restuarant.Category.Should().Be(command.Category);
            restuarant.HasDelivery.Should().Be(command.HasDelivery);
            restuarant.ContactEmail.Should().Be(command.ContactEmail);
            restuarant.ContactNumber.Should().Be(command.ContactNumber);
            restuarant.Address.Should().NotBeNull();
            restuarant.Address.City.Should().Be(command.City);
            restuarant.Address.Street.Should().Be(command.Street);
            restuarant.Address.PostalCode.Should().Be(command.PostalCode);
        }

        [Fact()]
        public void CreateMap_ForUpdateRestuarantCommandToRestuarant_MapsCorrectly()
        {
            // arrange
            var command = new UpdateRestuarantCommand
            {
                Id = 1,
                Name = "Test Updated Restuarant",
                Description = "Test Updated Description",
                HasDelivery = false,
            };

            // act
            var restuarant = _mapper.Map<Restuarant>(command);

            // assert
            restuarant.Should().NotBeNull();
            restuarant.Id.Should().Be(command.Id);
            restuarant.Name.Should().Be(command.Name);
            restuarant.Description.Should().Be(command.Description);
            restuarant.HasDelivery.Should().Be(command.HasDelivery);
        }
    }
}