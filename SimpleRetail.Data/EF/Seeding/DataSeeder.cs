using Bogus;
using SimpleRetail.Data.EF.Model;
using Person = SimpleRetail.Data.EF.Model.Person;

namespace SimpleRetail.Data.EF.Seeding;

public static class DataSeeder
{
    public static List<Person> GeneratePersons(int count)
    {
        var persons = new List<Person>();
        var faker = new Faker<Person>()
            .RuleFor(p => p.Code, f => f.Random.AlphaNumeric(6))
            .RuleFor(p => p.Name, f => f.Person.FullName)
            .RuleFor(p => p.Phone, f => f.Person.Phone)
            .RuleFor(p => p.Email, f => f.Person.Email)
            .RuleFor(p => p.InsertDate, DateTime.UtcNow)
            .RuleFor(p => p.InsertUser, Guid.NewGuid());

        for (int i = 0; i < count; i++)
        {
            persons.Add(faker.Generate());
        }

        return persons;
    }

    public static List<Store> GenerateStores(int count, List<Person> persons)
    {
        var stores = new List<Store>();
        var faker = new Faker<Store>()
            .RuleFor(s => s.Code, f => f.Random.AlphaNumeric(6))
            .RuleFor(s => s.Name, f => f.Company.CompanyName())
            .RuleFor(s => s.Location, f => f.Address.FullAddress())
            .RuleFor(s => s.Active, f => f.Random.Bool())
            .RuleFor(s => s.ContactId, f => f.PickRandom(persons).Id)
            .RuleFor(s => s.InsertDate, DateTime.UtcNow)
            .RuleFor(s => s.InsertUser, Guid.NewGuid());

        for (int i = 0; i < count; i++)
        {
            stores.Add(faker.Generate());
        }

        return stores;
    }

    public static List<Item> GenerateItems(int count)
    {
        var items = new List<Item>();
        var faker = new Faker<Item>()
            .RuleFor(i => i.Code, f => f.Random.AlphaNumeric(6))
            .RuleFor(i => i.Name, f => f.Commerce.ProductName())
            .RuleFor(i => i.Description, f => f.Commerce.ProductDescription())
            .RuleFor(i => i.Manufacturer, f => f.Company.CompanyName())
            .RuleFor(i => i.Active, f => f.Random.Bool())
            .RuleFor(i => i.InsertDate, DateTime.UtcNow)
            .RuleFor(i => i.InsertUser, Guid.NewGuid());

        for (int i = 0; i < count; i++)
        {
            items.Add(faker.Generate());
        }

        return items;
    }

    public static List<Supplier> GenerateSuppliers(int count, List<Person> persons)
    {
        var suppliers = new List<Supplier>();
        var faker = new Faker<Supplier>()
            .RuleFor(s => s.Code, f => f.Random.AlphaNumeric(6))
            .RuleFor(s => s.Name, f => f.Company.CompanyName())
            .RuleFor(s => s.LocationHQ, f => f.Address.City())
            .RuleFor(s => s.Active, f => f.Random.Bool())
            .RuleFor(s => s.ContactId, f => f.PickRandom(persons).Id)
            .RuleFor(s => s.InsertDate, DateTime.UtcNow)
            .RuleFor(s => s.InsertUser, Guid.NewGuid());

        for (int i = 0; i < count; i++)
        {
            suppliers.Add(faker.Generate());
        }

        return suppliers;
    }

    public static List<Procurement> GenerateProcurements(int count, List<Supplier> suppliers, List<Store> stores, List<Person> receivers)
    {
        var procurements = new List<Procurement>();
        var faker = new Faker<Procurement>()
            .RuleFor(p => p.Code, f => f.Random.AlphaNumeric(6))
            .RuleFor(p => p.DeliveryDate, f => f.Date.Future())
            .RuleFor(p => p.SupplierId, f => f.PickRandom(suppliers).Id)
            .RuleFor(p => p.StoreId, f => f.PickRandom(stores).Id)
            .RuleFor(p => p.ReceiverId, f => f.PickRandom(receivers).Id)
            .RuleFor(p => p.InsertDate, DateTime.UtcNow)
            .RuleFor(p => p.InsertUser, Guid.NewGuid());

        for (int i = 0; i < count; i++)
        {
            procurements.Add(faker.Generate());
        }

        return procurements;
    }

    public static List<ProcurementDetail> GenerateProcurementDetails(List<Procurement> procurements, List<Item> items)
    {
        var procurementDetails = new List<ProcurementDetail>();
        var faker = new Faker();

        foreach (var procurement in procurements)
        {
            var numberOfDetails = faker.Random.Int(1, 5); // Generate a random number of details for each procurement
            for (int i = 0; i < numberOfDetails; i++)
            {
                var detail = new ProcurementDetail
                {
                    ProcurementId = procurement.Id,
                    ItemId = faker.PickRandom(items).Id, 
                    ItemQuantity = faker.Random.Int(1, 10),
                    ItemUnitPrice = faker.Random.Decimal(1, 100),
                    InsertDate = DateTime.UtcNow,
                    InsertUser = Guid.NewGuid()
                };

                procurementDetails.Add(detail);
            }
        }

        return procurementDetails;
    }

    public static List<Order> GenerateOrders(int count, List<Person> customers, List<Store> stores)
    {
        var orders = new List<Order>();
        var faker = new Faker<Order>()
            .RuleFor(o => o.Code, f => f.Random.AlphaNumeric(6))
            .RuleFor(o => o.Created, f => f.Date.Past())
            .RuleFor(o => o.CustomerId, f => f.PickRandom(customers).Id)
            .RuleFor(o => o.StoreId, f => f.PickRandom(stores).Id)
            .RuleFor(o => o.PaymentMethod, f => f.Finance.CreditCardNumber())
            .RuleFor(o => o.TotalPrice, f => f.Random.Decimal(10, 1000))
            .RuleFor(o => o.TotalDiscount, f => f.Random.Decimal(0, 100))
            .RuleFor(o => o.TotalFee, f => f.Random.Decimal(0, 50))
            .RuleFor(o => o.DeliveryLocation, f => f.Address.FullAddress())
            .RuleFor(o => o.isCanceled, f => f.Random.Bool())
            .RuleFor(o => o.isCompleted, f => f.Random.Bool())
            .RuleFor(o => o.isDelivered, f => f.Random.Bool())
            .RuleFor(o => o.InsertDate, DateTime.UtcNow)
            .RuleFor(o => o.InsertUser, Guid.NewGuid());

        for (int i = 0; i < count; i++)
        {
            orders.Add(faker.Generate());
        }

        return orders;
    }

    public static List<OrderDetail> GenerateOrderDetails(List<Order> orders, List<Supplier> suppliers, List<Item> items)
    {
        var orderDetails = new List<OrderDetail>();
        var faker = new Faker();

        foreach (var order in orders)
        {
            var numberOfDetails = faker.Random.Int(1, 5); // Generate a random number of details for each order
            for (int i = 0; i < numberOfDetails; i++)
            {
                var detail = new OrderDetail
                {
                    OrderId = order.Id, 
                    SupplierId = faker.PickRandom(suppliers).Id, 
                    ItemId = faker.PickRandom(items).Id, 
                    ItemQuantity = faker.Random.Int(1, 10), 
                    ItemUnitPrice = faker.Random.Decimal(1, 100), 
                    InsertDate = DateTime.UtcNow, 
                    InsertUser = Guid.NewGuid() 
                };

                orderDetails.Add(detail);
            }
        }

        return orderDetails;
    }

    public static List<SupplierItem> GenerateSupplierStoreItems(int count, List<Supplier> suppliers, List<Store> stores, List<Item> items)
    {
        var supplierStoreItems = new List<SupplierItem>();

        var faker = new Faker<SupplierItem>()
            .RuleFor(s => s.SupplierId, f => f.PickRandom(suppliers).Id)
            .RuleFor(s => s.StoreId, f => f.PickRandom(stores).Id)
            .RuleFor(s => s.ItemId, f => f.PickRandom(items).Id)
            .RuleFor(s => s.Priority, f => f.Random.Number(10, 99));

        for (int i = 0; i < count; i++)
        {
            supplierStoreItems.Add(faker.Generate());
        }

        return supplierStoreItems;
    }
}
