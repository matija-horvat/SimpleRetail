using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleRetail.Data.EF.Model;
using SimpleRetail.Data.EF.Seeding;
using SimpleRetail.Data.Utils;
using System.Reflection;

namespace SimpleRetail.Data.EF;

public class DataContext: DbContext
{
    private readonly IConfiguration _configuration;
    private bool _isSeedingRequired = false;

    public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options)
    {
        _configuration = configuration;
    }

    //public DataContext(DbContextOptions<DataContext> options) : base(options)
    //{
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //apply to all configuration at once
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        //var connString = _configuration.GetConnectionString("DefaultConnection");

        //optionsBuilder.UseSqlServer(connString,
        //                        options => options.MigrationsAssembly("SimpleRetail.API"));
    }

    public void SeedData()
    {
        string filePath = "datasettings.json";

        // Load configuration from file
        DataConfig config = DataConfig.Load(filePath);

        _isSeedingRequired = Convert.ToBoolean(config.Options.NeedSeeding);

        if (_isSeedingRequired)
        {
            var persons = DataSeeder.GeneratePersons(15);
            var items = DataSeeder.GenerateItems(20);
            var suppliers = DataSeeder.GenerateSuppliers(5, persons);
            var stores = DataSeeder.GenerateStores(3, persons);

            var procurements = DataSeeder.GenerateProcurements(10, suppliers, stores, persons);
            var procurementDetails = DataSeeder.GenerateProcurementDetails(procurements, items);

            var orders = DataSeeder.GenerateOrders(10, persons, stores);
            var orderDetails = DataSeeder.GenerateOrderDetails(orders, suppliers, items);

            var supplierStoreItems = DataSeeder.GenerateSupplierStoreItems(15, suppliers, stores, items);

            this.AddRange(persons);
            this.AddRange(items);
            this.AddRange(suppliers);
            this.AddRange(stores);
            this.AddRange(procurements);
            this.AddRange(procurementDetails);
            this.AddRange(orders);
            this.AddRange(orderDetails);
            this.AddRange(supplierStoreItems);

            this.SaveChanges();

            config.Options.NeedSeeding = "false";

            config.Save(filePath);
        }
    }

    public DbSet<Person> Persons { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Procurement> Procurements { get; set; }
    public DbSet<ProcurementDetail> ProcurementDetails { get; set; }
    public DbSet<SupplierItem> SupplierItems { get; set; }
}
