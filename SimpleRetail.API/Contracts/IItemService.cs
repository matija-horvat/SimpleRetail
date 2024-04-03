using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Contracts;

public interface IItemService: IService<ChangeItemRequest, ItemDto>
{
}
