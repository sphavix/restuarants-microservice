using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restuarants.Application.Users;
using Restuarants.Application.Users.Abstract;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Restuarants.Infrastructure.Authorization.Requirements.Tests
{
    public class CreatedMultipleRestuarantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestuarants_ShouldSucceed()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(um => um.GetCurrentUser()).Returns(currentUser);

            var restuarants = new List<Restuarant>()
            {
                new()
                {
                    OwnerId = currentUser.Id
                },
                new()
                {
                    OwnerId = currentUser.Id
                },
                new()
                {
                    OwnerId = "2",
                }
            };

            var restuarantsRepositoryMock = new Mock<IRestuarantsRepository>();
            restuarantsRepositoryMock.Setup(rm => rm.GetRestuarantsAsync())
                .ReturnsAsync(restuarants); // returns 3 restuarants, 2 of which are owned by the current user

            var requirement = new CreatedMultipleRestuarantsRequirement(2);
            var handler = new CreatedMultipleRestuarantsRequirementHandler(
                restuarantsRepositoryMock.Object, userContextMock.Object); // inject the mock repository

            var context = new AuthorizationHandlerContext(
                [requirement], null, null); // create a new context 


            // act
            await handler.HandleAsync(context);

            // assert
            context.HasSucceeded.Should().BeTrue();
        }

        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestuarants_ShouldFail()
        {
            var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);
            var userContextMock = new Mock<IUserContext>();
            userContextMock.Setup(um => um.GetCurrentUser()).Returns(currentUser);

            var restuarants = new List<Restuarant>()
            {
                new()
                {
                    OwnerId = currentUser.Id
                },
                new()
                {
                    OwnerId = "2",
                }
            };

            var restuarantsRepositoryMock = new Mock<IRestuarantsRepository>();
            restuarantsRepositoryMock.Setup(rm => rm.GetRestuarantsAsync())
                .ReturnsAsync(restuarants); // returns 3 restuarants, 2 of which are owned by the current user

            var requirement = new CreatedMultipleRestuarantsRequirement(2);
            var handler = new CreatedMultipleRestuarantsRequirementHandler(
                restuarantsRepositoryMock.Object, userContextMock.Object); // inject the mock repository

            var context = new AuthorizationHandlerContext(
                [requirement], null, null); // create a new context 


            // act
            await handler.HandleAsync(context);

            // assert
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();
        }
    }
}