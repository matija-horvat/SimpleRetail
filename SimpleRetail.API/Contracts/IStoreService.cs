using Azure;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Contracts;

public interface IStoreService : IService<ChangeStoreRequest, StoreDto>
{
    /// <summary>
    /// Return specific entity by Id and include sub entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<StoreDto?> GetDetailsById(Guid id);
}
