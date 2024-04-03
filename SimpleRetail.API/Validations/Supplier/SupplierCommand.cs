﻿using MediatR;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Validations.Supplier;

public class SupplierCommand : IRequest<SupplierDto>
{
    public ChangeSupplierRequest Request { get; }

    public ChangeAction Action { get; set; } = ChangeAction.NONE;

    public SupplierCommand(ChangeSupplierRequest request, ChangeAction action = ChangeAction.NONE)
    {
        Request = request;
        Action = action;
    }
}
