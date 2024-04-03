using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleRetail.Data.EF;

namespace SimpleRetail.Data.Utils;

public class DataSeederService: BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public DataSeederService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            await dbContext.Database.MigrateAsync(stoppingToken); // Ensure the database is created and migrated
            dbContext.SeedData(); // Seed data if needed
        }
    }
}
