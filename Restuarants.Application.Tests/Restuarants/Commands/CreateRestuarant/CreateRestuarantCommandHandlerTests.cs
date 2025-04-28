using Xunit;
using Moq;
using AutoMapper;
using Restuarants.Domain.Repositories;
using Restuarants.Domain.Entities;
using Microsoft.Extensions.Logging;
using Restuarants.Application.Users.Abstract;
using Restuarants.Application.Users;
using FluentAssertions;

namespace Restuarants.Application.Restuarants.Commands.CreateRestuarant.Tests
{
    public class CreateRestuarantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_RestuarantCreatedRestuarantId()
        {
            // arrange
            
            var loggerMock = new Mock<ILogger<CreateRestuarantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestuarantCommand();
            var restuarant = new Restuarant();
            mapperMock.Setup(m => m.Map<Restuarant>(command))
                .Returns(restuarant);

            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("owner-id", "test@gmail.com", [], null, null);
            userContextMock.Setup(uc => uc.GetCurrentUser())
                .Returns(currentUser);

            var restuarantsRepositoryMock = new Mock<IRestuarantsRepository>();
            restuarantsRepositoryMock.Setup(repo => repo.CreateRestuarantAsync(It.IsAny<Restuarant>()))
                .ReturnsAsync(1);

            var commandHandler = new CreateRestuarantCommandHandler(loggerMock.Object, 
                restuarantsRepositoryMock.Object, mapperMock.Object, userContextMock.Object);


            // act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(1);
            restuarant.OwnerId.Should().Be("owner-id");
            restuarantsRepositoryMock.Verify(repo => repo.CreateRestuarantAsync(restuarant), Times.Once);
        }
    }
}