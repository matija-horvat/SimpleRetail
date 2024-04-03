using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.BL.Contracts;

public interface IItemService: IService<ChangeItemRequest, ItemDto>
{
}
