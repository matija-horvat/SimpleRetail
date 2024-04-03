using MediatR;
using SimpleRetail.BL.Validations.Enums;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.BL.Validations.Item;

public class ItemCommandHandler : IRequestHandler<ItemCommand, ItemDto>
{
    private readonly IItemRepository _ItemRepository;

    public ItemCommandHandler(IItemRepository ItemRepository)
    {
        _ItemRepository = ItemRepository;
    }

    public async Task<ItemDto> Handle(ItemCommand command, CancellationToken cancellationToken)
    {
        ItemDto dto = new ItemDto();
        switch (command.Action)
        {
            case ChangeAction.INSERT:
                dto = await _ItemRepository.CreateAsync(command.Request);
                break;
            case ChangeAction.UPDATE:
                dto = await _ItemRepository.UpdateAsync(command.Request);
                break;
            default:
                break;
        }

        return dto;
    }
}
