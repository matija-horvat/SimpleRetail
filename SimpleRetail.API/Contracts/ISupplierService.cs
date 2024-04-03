using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Contracts;

public interface ISupplierService: IService<ChangeSupplierRequest, SupplierDto>
{
    /// <summary>
    /// Return specific entity by Id and include sub entities.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SupplierDto?> GetDetailsById(Guid id);
}
