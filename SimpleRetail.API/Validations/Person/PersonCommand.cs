using MediatR;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Validations.Person;

public class PersonCommand : IRequest<PersonDto>
{
    public ChangePersonRequest Request { get; }

    public ChangeAction Action { get; set; } = ChangeAction.NONE;

    public PersonCommand(ChangePersonRequest request, ChangeAction action = ChangeAction.NONE)
    {
        Request = request;
        Action = action;
    }
}
