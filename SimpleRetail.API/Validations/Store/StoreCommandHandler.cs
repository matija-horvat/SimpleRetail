using MediatR;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Validations.Store;

public class StoreCommandHandler : IRequestHandler<StoreCommand, StoreDto>
{
    private readonly IStoreRepository _storeRepository;

    public StoreCommandHandler(IStoreRepository storeRepository)
    {
        _storeRepository = storeRepository;
    }

    public async Task<StoreDto> Handle(StoreCommand command, CancellationToken cancellationToken)
    {
        StoreDto dto = new StoreDto();
        switch (command.Action)
        {
            case ChangeAction.INSERT:
                dto = await _storeRepository.CreateAsync(command.Request);
                break;
            case ChangeAction.UPDATE:
                dto = await _storeRepository.UpdateAsync(command.Request);
                break;
            default:
                break;
        }

        return dto;
    }
}
