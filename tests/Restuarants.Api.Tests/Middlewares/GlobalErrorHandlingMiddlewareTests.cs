using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restuarants.Domain.Entities;
using Restuarants.Domain.Exceptions;
using System.Threading.Tasks;
using Xunit;

namespace Restuarants.Api.Middlewares.Tests
{
    public class GlobalErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionsThrow_ShouldCallNextDelegate()
        {
            // arrange
            var loggerMock = new Mock<ILogger<GlobalErrorHandlingMiddleware>>();
            var middleware = new GlobalErrorHandlingMiddleware(loggerMock.Object);

            var context = new DefaultHttpContext();
            var nextDelegate = new Mock<RequestDelegate>();

            // act
            await middleware.InvokeAsync(context, nextDelegate.Object);

            // assert   
            nextDelegate.Verify(x => x.Invoke(context), Times.Once); // This verifies that the next delegate was called exactly once.
        }

        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldReturnStatusCode404()
        {
            // arrange
            var loggerMock = new Mock<ILogger<GlobalErrorHandlingMiddleware>>();
            var middleware = new GlobalErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var notFoundException = new NotFoundException(nameof(Restuarant), "1");
            

            // act
            await middleware.InvokeAsync(context, _ => throw notFoundException);


            // assert
            context.Response.StatusCode.Should().Be(404);
        }

        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldReturnStatusCode403()
        {
            // arrange
            var loggerMock = new Mock<ILogger<GlobalErrorHandlingMiddleware>>();
            var middleware = new GlobalErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var forbidException = new ForbidException();


            // act
            await middleware.InvokeAsync(context, _ => throw forbidException);


            // assert
            context.Response.StatusCode.Should().Be(403);
        }

        [Fact()]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldReturnStatusCode500()
        {
            // arrange
            var loggerMock = new Mock<ILogger<GlobalErrorHandlingMiddleware>>();
            var middleware = new GlobalErrorHandlingMiddleware(loggerMock.Object);
            var context = new DefaultHttpContext();
            var exception = new Exception();


            // act
            await middleware.InvokeAsync(context, _ => throw exception);


            // assert
            context.Response.StatusCode.Should().Be(500);
        }
    }
}