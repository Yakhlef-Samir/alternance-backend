namespace Alternance.Api.Middlewares;

public class CacheMiddleware
{
    private readonly RequestDelegate _next;

    public CacheMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Cache logic here
        await _next(context);
    }
}
