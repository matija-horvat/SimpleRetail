using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SimpleRetail.API.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyName = "X-API-KEY";
    private const string ApiKey = "SimpleRetail";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue(ApiKeyName, out var apiKey) || apiKey != ApiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";
            var errorObj = new
            {
                error = "Invalid API Key"
            };

            var jsonResponse = JsonConvert.SerializeObject(errorObj);

            await context.Response.WriteAsync(jsonResponse);
            return;
        }

        await _next(context);
    }
}
