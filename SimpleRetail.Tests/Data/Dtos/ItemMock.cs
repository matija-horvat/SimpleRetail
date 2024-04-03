using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Tests.Data.Dtos;

public class ItemMock
{
    public static Item Get()
    {
        var item = new Item()
        {
            Id = Globals.itemId,
            Active = true,
            Name = "Test",
            Description = "Test"
        };

        return item;
    }

    public static IEnumerable<Item> GetList()
    {
        var items = new List<Item>() { new Item()
        {
            Id = Globals.itemId,
            Active = true,
            Name = "Test",
            Description = "Test"
        }};

        return items;
    }

    public static IEnumerable<Item> GetEmptyList()
    {
        var items = new List<Item>();
        return items;
    }
}
