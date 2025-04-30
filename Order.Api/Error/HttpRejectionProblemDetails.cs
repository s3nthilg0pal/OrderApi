using Microsoft.AspNetCore.Mvc;

namespace Order.Api.Error;

public class HttpRejectionProblemDetails : ValidationProblemDetails
{
    public HttpRejectionProblemDetails(HttpContext httpContext)
    {
        Title = "Bad Request";
        Status = StatusCodes.Status400BadRequest;
        Detail = "HTTP requests are not allowed. Please use HTTPS.";
        Instance = $"{httpContext.Request.Path} ({httpContext.
            TraceIdentifier})";

        Dictionary<string, string?> relevantHeaders = new
            Dictionary<string, string?>
            {
                {"Host", httpContext.Request.Headers["Host"]},
                {"User-Agent", httpContext.Request.Headers[
                    "UserAgent"]},
                {"X-Forwarded-Proto", httpContext.Request.Headers[
                    "XForwarded-Proto"]},
                {"X-Forwarded-For", httpContext.Request.Headers[
                    "XForwarded-For"]}
            };
        Extensions["headers"] = relevantHeaders;
    }
}