using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace SimpleRetail.API.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApiKeyName = "X-API-KEY";
    private const string ApiKey = "SimpleRetail";
    private readonly ILogger<ApiKeyMiddleware> _logger;

    public ApiKeyMiddleware(RequestDelegate next, ILogger<ApiKeyMiddleware> logger)
    {
        _next = next;
        _logger = logger;
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

            _logger.LogError("Invalid API Key");

            var jsonResponse = JsonConvert.SerializeObject(errorObj);

            await context.Response.WriteAsync(jsonResponse);
            return;
        }

        await _next(context);
    }
}
