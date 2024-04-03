using MediatR;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Validations.Supplier;

public class SupplierCommandHandler : IRequestHandler<SupplierCommand, SupplierDto>
{
    private readonly ISupplierRepository _SupplierRepository;

    public SupplierCommandHandler(ISupplierRepository SupplierRepository)
    {
        _SupplierRepository = SupplierRepository;
    }

    public async Task<SupplierDto> Handle(SupplierCommand command, CancellationToken cancellationToken)
    {
        SupplierDto dto = new SupplierDto();
        switch (command.Action)
        {
            case ChangeAction.INSERT:
                dto = await _SupplierRepository.CreateAsync(command.Request);
                break;
            case ChangeAction.UPDATE:
                dto = await _SupplierRepository.UpdateAsync(command.Request);
                break;
            default:
                break;
        }

        return dto;
    }
}
