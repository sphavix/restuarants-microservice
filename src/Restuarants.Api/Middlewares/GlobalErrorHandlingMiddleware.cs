﻿
using Restuarants.Domain.Exceptions;

namespace Restuarants.Api.Middlewares
{
    public class GlobalErrorHandlingMiddleware(ILogger<GlobalErrorHandlingMiddleware> _logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundException apiError)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(apiError.Message);

                _logger.LogWarning(apiError.Message);
            }
            catch(ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Access Forbidden");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;

                await context.Response.WriteAsync("Oppsie! Something went wrong, please contact your system administrator");
            }
        }
    }
}
