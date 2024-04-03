using Microsoft.AspNetCore.Http.Json;
using SimpleRetail.Common.Errors;

namespace SimpleRetail.Common;

public static class Startup
{
    public static IServiceCollection AddCommon(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new SimpleRetailExceptionConverter());
        });

        return services;
    }

    public static void Main(string[] args)
    {

    }
}