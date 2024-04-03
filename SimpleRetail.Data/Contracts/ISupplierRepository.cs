using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.Data.Contracts;

public interface ISupplierRepository : IRepository<ChangeSupplierRequest, SupplierDto>
{
    /// <summary>
    /// Return specific entity by Id and include sub entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SupplierDto?> GetDetailsById(Guid id);
}
