using Azure;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.Data.Contracts;

public interface IItemRepository: IRepository<ChangeItemRequest, ItemDto>
{

}
