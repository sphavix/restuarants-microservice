using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restuarants.Domain.Abstracts;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Exceptions;
using Restuarants.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Restuarants.Application.Restuarants.Commands.UpdateRestuarant.Tests
{
    public class UpdateRestuarantCommandHandlerTests
    {
        private readonly Mock<ILogger<UpdateRestuarantCommandHandler>> _loggerMock;
        private readonly Mock<IRestuarantsRepository> _restuarantsRepositoryMock;
        private readonly Mock<IRestuarantAuthorizationService> _restuarantAuthorizationServiceMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateRestuarantCommandHandler _commandHandler;

        public UpdateRestuarantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestuarantCommandHandler>>();
            _restuarantsRepositoryMock = new Mock<IRestuarantsRepository>();
            _restuarantAuthorizationServiceMock = new Mock<IRestuarantAuthorizationService>();
            _mapperMock = new Mock<IMapper>();

            _commandHandler = new UpdateRestuarantCommandHandler(
                _restuarantsRepositoryMock.Object,
                _loggerMock.Object,
                _restuarantAuthorizationServiceMock.Object,
                _mapperMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequests_ShouldUpdateRestuarants()
        {
            // arrange
            var restuarantId = 1;

            var command = new UpdateRestuarantCommand
            {
                Id = restuarantId,
                Name = "Updated Restuarant",
                Description = "Updated Description",
                HasDelivery = true
            };

            var restuarant = new Restuarant
            {
                Id = restuarantId,
                Name = "Old Restuarant",
                Description = "Old Description"
            };

            _restuarantsRepositoryMock
                .Setup(repo => repo.GetRestuarantAsync(restuarantId))
                .ReturnsAsync(restuarant);

            // get pass the authorization check
            _restuarantAuthorizationServiceMock
                .Setup(auth => auth.Authorize(restuarant, Domain.Constants.ResourceOperation.Update))
                .Returns(true);

            // act
            await _commandHandler.Handle(command, CancellationToken.None);

            // assert
            _restuarantsRepositoryMock.Verify(repo => repo.SaveChanges(), Times.Once); // check if the save changes was called
            _mapperMock.Verify(m => m.Map(command, restuarant), Times.Once); // check if the mapping was called
        }

        [Fact()]
        public async Task Handle_WithNonExistingRestuarant_ShouldThrowNotFoundException()
        {
            // arrange
            var restuarantId = 999; // non-existing id
            var command = new UpdateRestuarantCommand
            {
                Id = restuarantId
            };


            _restuarantsRepositoryMock
                .Setup(repo => repo.GetRestuarantAsync(restuarantId))
                .ReturnsAsync((Restuarant?)null); // simulate not found


            // act
            Func<Task> act = async () => await _commandHandler.Handle(command, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restuarant with {restuarantId} does not exist, please try again");

        }

        [Fact()]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbidException()
        {
            // arrange
            var restuarantId = 3;
            var command = new UpdateRestuarantCommand
            {
                Id = restuarantId
            };
            var restuarant = new Restuarant
            {
                Id = restuarantId
            };

            // simulate getting the restuarant
            _restuarantsRepositoryMock
                .Setup(repo => repo.GetRestuarantAsync(restuarantId))
                .ReturnsAsync(restuarant);
            // simulate authorization failure
            _restuarantAuthorizationServiceMock
                .Setup(auth => auth.Authorize(restuarant, Domain.Constants.ResourceOperation.Update))
                .Returns(false);

            // act
            Func<Task> act = async () => await _commandHandler.Handle(command, CancellationToken.None);
            // assert
            await act.Should().ThrowAsync<ForbidException>();
        }
    }
}