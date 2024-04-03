using Microsoft.EntityFrameworkCore;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Seeding;
using SimpleRetail.Tests.Data.Dtos;

namespace SimpleRetail.Tests.Data;

public class DataDbContextFactoryMock : IDbContextFactory<DataContext>
{
    private DbContextOptions<DataContext> _options;

    public DataDbContextFactoryMock(string databaseName = "InMemory")
    {
        _options = new DbContextOptionsBuilder<DataContext>()
                        .UseInMemoryDatabase(databaseName)
                        .Options;
    }

    public DataContext CreateDbContext()
    {
        var context = new DataContext(null, _options);

        if (context.Items.Find(ItemMock.Get().Id) == null)
            context.Items.Add(ItemMock.Get());

        context.SaveChanges();
        return context;
    }
}
