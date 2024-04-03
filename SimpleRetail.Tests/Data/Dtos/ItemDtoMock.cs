using SimpleRetail.Common.Responses;

namespace SimpleRetail.Tests.Data.Dtos;

public class ItemDtoMock
{
    public static ItemDto Get()
    {
        var item = new ItemDto()
        {
            Id = Globals.itemId,
            Active = true,
            Name = "Test",
            Description = "Test"
        };

        return item;
    }

    public static IEnumerable<ItemDto> GetList()
    {
        var items = new List<ItemDto>() { new ItemDto()
    {
        Id = Globals.itemId,
        Active = true,
        Name = "Test",
        Description = "Test"
    }};

        return items;
    }

    public static IEnumerable<ItemDto> GetEmptyList()
    {
        var items = new List<ItemDto>();
        return items;
    }
}
