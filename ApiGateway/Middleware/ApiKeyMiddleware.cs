namespace ApiGateway.Middleware;
public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _config;
    private const string ApiKeyHeader = "X-Api-Key";
    public ApiKeyMiddleware(RequestDelegate next, IConfiguration config)
    {
        _next = next;
        _config = config;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var key)
            || key != _config["ApiGateway:ApiKey"])
        {
            context.Response.StatusCode = 401;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync("{\"error\":\"Invalid or missing API key.\"}");
            return;
        }
        await _next(context);
    }
}