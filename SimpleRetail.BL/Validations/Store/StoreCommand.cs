using MediatR;
using SimpleRetail.BL.Validations.Enums;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.BL.Validations.Store;

public class StoreCommand: IRequest<StoreDto>
{
    public ChangeStoreRequest Request { get; }

    public ChangeAction Action { get; set; } = ChangeAction.NONE;

    public StoreCommand(ChangeStoreRequest request, ChangeAction action = ChangeAction.NONE)
    {
        Request = request;
        Action = action;
    }
}

