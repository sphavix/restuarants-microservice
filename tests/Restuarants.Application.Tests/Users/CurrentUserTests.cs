﻿using FluentAssertions;
using Restuarants.Domain.Constants;
using Xunit;

namespace Restuarants.Application.Users.Tests
{
    public class CurrentUserTests
    {
        // TestMethod_Scenario_ExpectedResult
        [Theory()]
        [InlineData(UserRole.Admin)]
        [InlineData(UserRole.User)]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue(string roleName)
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User], null, null);

            // act
            var isInRole = currentUser.IsInRole(roleName);


            // assert
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User], null, null);

            // act
            var isInRole = currentUser.IsInRole(UserRole.Owner);


            // assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithMatchingRoleCase_ShouldReturnTrue()
        {
            // arrange
            var currentUser = new CurrentUser("1", "test@test.com", [UserRole.Admin, UserRole.User], null, null);

            // act
            var isInRole = currentUser.IsInRole(UserRole.Admin.ToLower());


            // assert
            isInRole.Should().BeFalse();
        }
    }
}