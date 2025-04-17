﻿using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restuarants.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restuarants.Application.Users.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // arrange
            var dateOfBirth = new DateOnly(1995, 1, 1);
            
            var httpContextAssessorMock = new Mock<IHttpContextAccessor>();
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "123"),
                new(ClaimTypes.Email, "test@test.com"),
                new(ClaimTypes.Role, UserRole.Admin),
                new(ClaimTypes.Role, UserRole.User),
                new("Nationality", "South African"),
                new("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAssessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAssessorMock.Object);

            // act
            var currentUser = userContext.GetCurrentUser();


            // assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("123");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Roles.Should().Contain( UserRole.Admin, UserRole.User);
            currentUser.Nationality.Should().Be("South African");
            currentUser.DateOfBirth.Should().Be(dateOfBirth);
        }

        [Fact]
        public void GetCurrentUser_WithUserContextNotPresent_ThrowsInvalidOperationException()
        {
            // arrange
            var httpContextAssessorMock = new Mock<IHttpContextAccessor>();

            httpContextAssessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

            var userContext = new UserContext(httpContextAssessorMock.Object);

            // act

            Action action = () => userContext.GetCurrentUser();


            // assert
            action.Should().Throw<InvalidOperationException>()
                .WithMessage("User context is not present");
        }
    }
}