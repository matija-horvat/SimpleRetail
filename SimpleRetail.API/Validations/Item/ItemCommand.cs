using MediatR;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Validations.Item;

public class ItemCommand : IRequest<ItemDto>
{
    public ChangeItemRequest Request { get; }

    public ChangeAction Action { get; set; } = ChangeAction.NONE;

    public ItemCommand(ChangeItemRequest request, ChangeAction action = ChangeAction.NONE)
    {
        Request = request;
        Action = action;
    }
}
