using MediatR;
using SimpleRetail.BL.Validations.Enums;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.BL.Validations.Person;

public class PersonCommandHandler : IRequestHandler<PersonCommand, PersonDto>
{
    private readonly IPersonRepository _PersonRepository;

    public PersonCommandHandler(IPersonRepository PersonRepository)
    {
        _PersonRepository = PersonRepository;
    }

    public async Task<PersonDto> Handle(PersonCommand command, CancellationToken cancellationToken)
    {
        PersonDto dto = new PersonDto();
        switch (command.Action)
        {
            case ChangeAction.INSERT:
                dto = await _PersonRepository.CreateAsync(command.Request);
                break;
            case ChangeAction.UPDATE:
                dto = await _PersonRepository.UpdateAsync(command.Request);
                break;
            default:
                break;
        }

        return dto;
    }
}
