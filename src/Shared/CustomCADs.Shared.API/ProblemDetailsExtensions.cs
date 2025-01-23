using Microsoft.AspNetCore.Http;

namespace CustomCADs.Shared.API;

using static StatusCodes;

public static class ProblemDetailsExtensions
{
    public static async Task<bool> BadRequestResponseAsync(this IProblemDetailsService service, HttpContext context, Exception ex, string message = "Invalid Request Parameters")
    {
        context.Response.StatusCode = Status400BadRequest;

        return await service.TryWriteAsync(new()
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new()
            {
                Type = ex.GetType().Name,
                Title = message,
                Detail = ex.Message,
                Status = Status400BadRequest,
            },
        }).ConfigureAwait(false);
    }

    public static async Task<bool> UnauthorizedResponseAsync(this IProblemDetailsService service, HttpContext context, Exception ex, string message = "Inappropriately Unauthenticated")
    {
        context.Response.StatusCode = Status401Unauthorized;

        return await service.TryWriteAsync(new()
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new()
            {
                Type = ex.GetType().Name,
                Title = message,
                Detail = ex.Message,
                Status = Status401Unauthorized,
            },
        }).ConfigureAwait(false);
    }

    public static async Task<bool> ForbiddenResponseAsync(this IProblemDetailsService service, HttpContext context, Exception ex, string message = "Authorization Issue")
    {
        context.Response.StatusCode = Status403Forbidden;

        return await service.TryWriteAsync(new()
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new()
            {
                Type = ex.GetType().Name,
                Title = message,
                Detail = ex.Message,
                Status = Status403Forbidden,
            },
        }).ConfigureAwait(false);
    }

    public static async Task<bool> NotFoundResponseAsync(this IProblemDetailsService service, HttpContext context, Exception ex, string message = "Resource Not Found")
    {
        context.Response.StatusCode = Status404NotFound;

        return await service.TryWriteAsync(new()
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new()
            {
                Type = ex.GetType().Name,
                Title = message,
                Detail = ex.Message,
                Status = Status404NotFound,
            },
        }).ConfigureAwait(false);
    }

    public static async Task<bool> InternalServerErrorResponseAsync(this IProblemDetailsService service, HttpContext context, Exception ex, string message = "Internal Error")
    {
        context.Response.StatusCode = Status500InternalServerError;

        return await service.TryWriteAsync(new()
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new()
            {
                Type = ex.GetType().Name,
                Title = message,
                Detail = ex.Message,
                Status = Status500InternalServerError,
            },
        }).ConfigureAwait(false);
    }

    public static async Task<bool> CusotmResponseAsync(this IProblemDetailsService service, HttpContext context, Exception ex, int status = Status400BadRequest, string message = "Error processing request")
    {
        context.Response.StatusCode = status;

        return await service.TryWriteAsync(new()
        {
            HttpContext = context,
            Exception = ex,
            ProblemDetails = new()
            {
                Type = ex.GetType().Name,
                Title = message,
                Detail = ex.Message,
                Status = status,
            },
        }).ConfigureAwait(false);
    }
}
