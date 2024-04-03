using MediatR;
using SimpleRetail.API.Contracts;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.API.Validations.Store;
using SimpleRetail.Common.Requests;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Services;

public class StoreService: IStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly IMediator _mediator;

    public StoreService(IStoreRepository storeRepository, IMediator mediator)
    {
        _storeRepository = storeRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<StoreDto>> GetAll(int page, int recordsToTake)
    {
        return await _storeRepository.GetAll(page, recordsToTake);
    }

    public async Task<StoreDto?> GetById(Guid id)
    {
        return await _storeRepository.GetById(id);
    }

    public async Task<StoreDto?> GetDetailsById(Guid id)
    {
        return await _storeRepository.GetDetailsById(id);
    }

    public async Task<StoreDto?> CreateAsync(ChangeStoreRequest request)
    {
        request.Id = Guid.NewGuid();
        request.Active = true;

        var command = new StoreCommand(request, ChangeAction.INSERT);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<StoreDto?> UpdateAsync(ChangeStoreRequest request)
    {
        var command = new StoreCommand(request, ChangeAction.UPDATE);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
    {
        await _storeRepository.DeleteAsync(id, ChangeUserId);
        return;
    }


}
