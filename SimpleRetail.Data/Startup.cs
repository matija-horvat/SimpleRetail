using Microsoft.Extensions.DependencyInjection;
using SimpleRetail.Common.Requests;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.Repositories;
using SimpleRetail.Common.Errors;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Data.Utils;
using Microsoft.Extensions.Configuration;

namespace SimpleRetail.Data;

public static class Startup
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<DataContext>(options =>
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString, options => options.MigrationsAssembly("SimpleRetail.API"));
            
            //.EnableSensitiveDataLogging(true);
        });

        services.AddHostedService<DataSeederService>();


        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IStatisticsRepository, StatisticsRepository>();
        services.AddScoped<ISupplierStoreItemRepository, SupplierStoreItemRepository>();

        services.AddAutoMapper(typeof(Startup).Assembly);

        //services.AddScoped(typeof(IRepository<,>), typeof(CrudRepository<,,>));
        //services.AddScoped<IRepository<ChangePersonRequest, PersonDto>, PersonRepository>();
        //services.AddScoped<IRepository<ChangeItemRequest, ItemDto>, ItemRepository>();
        //services.AddScoped<IRepository<ChangeStoreRequest, StoreDto>, StoreRepository>();
        //services.AddScoped<IRepository<ChangeSupplierRequest, SupplierDto>, SupplierRepository>();

        //services.AddScoped(typeof(IRepository<,,>), typeof(CrudRepository<,,>));
        //services.AddScoped<IRepository<PersonDto, ChangePersonRequest, PersonDto>, PersonRepository>();
        //services.AddScoped<IRepository<ItemDto, ChangeItemRequest, ItemDto>, ItemRepository>();
        //services.AddScoped<IRepository<StoreDto, ChangeStoreRequest, StoreDto>, StoreRepository>();
        //services.AddScoped<IRepository<SupplierDto, ChangeSupplierRequest, SupplierDto>, SupplierRepository>();


        return services;
    }
}