using SimpleRetail.Common.Requests;

namespace SimpleRetail.Tests.Common.Requests;

public class ChangeItemRequestMock
{
    public static ChangeItemRequest Get()
    {
        var request = new ChangeItemRequest()
        {
            Active = true,
            ChangeUserId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test"
        };

        return request;
    }

    public static ChangeItemRequest GetInValid()
    {
        var request = new ChangeItemRequest()
        {
            Active = true,
            //ChangeUserId = Guid.NewGuid(),
            Id = Guid.NewGuid(),
            Name = "Test",
            Description = "Test"
        };

        return request;
    }
}
