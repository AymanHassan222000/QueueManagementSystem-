namespace QMS.API.Middleware;

public class LoggingScopeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingScopeMiddleware> _logger;

    public LoggingScopeMiddleware(
        RequestDelegate next,
        ILogger<LoggingScopeMiddleware> logger
    ) 
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) 
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "anonymous";
        var requestId = context.TraceIdentifier;
        var action = context.GetEndpoint()?.DisplayName ?? "unkown";
        var userAgent = context.Request.Headers.UserAgent.ToString();
        var ipAddress = context.Connection.RemoteIpAddress?.ToString();

        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["RequestId"] = requestId,
            ["UserId"] = userId,
            ["Action"] = action,
            ["UserAgent"] = userAgent,
            ["IPAddress"] = ipAddress
        }))
        {
            await _next(context);
        }

    }
}
