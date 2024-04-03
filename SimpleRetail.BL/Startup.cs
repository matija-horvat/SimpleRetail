using FluentValidation;
using MediatR;
using SimpleRetail.BL.Behaviors;
using SimpleRetail.BL.Contracts;
using SimpleRetail.BL.Services;
using System.Reflection;

public static class Startup
{
    public static IServiceCollection AddBL(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IPersonService, PersonService>();
        services.AddScoped<IStoreService, StoreService>();
        services.AddScoped<ISupplierService, SupplierService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<ISupplierStoreItemService, SupplierStoreItemService>();

        return services;
    }

    public static void Main(string[] args)
    {

    }
}
