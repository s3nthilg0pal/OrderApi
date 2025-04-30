using Order.Api.Error;

namespace Order.Api.Middleware;

public class HttpOnlyMiddleware
{
    private readonly RequestDelegate _next;

    public HttpOnlyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.IsHttps)
        {
            var problemDetails = new
                HttpRejectionProblemDetails(context);
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = @"application/problem + json";
            await context.Response.WriteAsJsonAsync(problemDetails);
            return;
        }

        await _next(context);
    }
}