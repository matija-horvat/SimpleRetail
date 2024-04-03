using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Contracts;

public interface ISupplierStoreItemService
{
    /// <summary>
    /// Return all members of specified entity.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="recordsToTake"></param>
    /// <returns></returns>
    Task<IEnumerable<SupplierStoreItemDto>> GetAll(int page, int recordsToTake);

    /// <summary>
    /// Save new entity into database.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<SupplierStoreItemDto?> CreateAsync(SupplierStoreItemDto request);

    /// <summary>
    /// Delete existing entity from database (soft method).
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task DeleteAsync(SupplierStoreItemDeleteRequest request);
}
