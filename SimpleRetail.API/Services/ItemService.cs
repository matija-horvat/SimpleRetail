using MediatR;
using SimpleRetail.API.Contracts;
using SimpleRetail.API.Validations.Enums;
using SimpleRetail.API.Validations.Item;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;

namespace SimpleRetail.API.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly IMediator _mediator;

    public ItemService(IItemRepository itemRepository, IMediator mediator)
    {
        _itemRepository = itemRepository;
        _mediator = mediator;
    }

    public async Task<IEnumerable<ItemDto>> GetAll(int page, int recordsToTake)
    {
        return await _itemRepository.GetAll(page, recordsToTake);
    }

    public async Task<ItemDto?> GetById(Guid id)
    {
        return await _itemRepository.GetById(id);
    }

    public async Task<ItemDto?> CreateAsync(ChangeItemRequest request)
    {
        request.Id = Guid.NewGuid();
        request.Active = true;

        var command = new ItemCommand(request, ChangeAction.INSERT);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task<ItemDto?> UpdateAsync(ChangeItemRequest request)
    {
        var command = new ItemCommand(request, ChangeAction.UPDATE);
        var result = await _mediator.Send(command);
        return result;
    }

    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
    {
        await _itemRepository.DeleteAsync(id, ChangeUserId);
        return;
    }
}
