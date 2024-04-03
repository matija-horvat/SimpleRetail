using MediatR;
using SimpleRetail.API.Contracts;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.API.Validations.Person;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;

namespace SimpleRetail.API.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMediator _mediator;

    public PersonService(IPersonRepository personRepository, IMediator mediator)
    {
        _personRepository = personRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<PersonDto>> GetAll(int page, int recordsToTake)
    {
        return await _personRepository.GetAll(page, recordsToTake);
    }

    public async Task<PersonDto?> GetById(Guid id)
    {
        return await _personRepository.GetById(id);
    }

    public async Task<PersonDto?> CreateAsync(ChangePersonRequest request)
    {
        request.Id = Guid.NewGuid();
        request.Active = true;

        var command = new PersonCommand(request, ChangeAction.INSERT);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<PersonDto?> UpdateAsync(ChangePersonRequest request)
    {
        var command = new PersonCommand(request, ChangeAction.UPDATE);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
    {
        await _personRepository.DeleteAsync(id, ChangeUserId);
        return;
    }
}
