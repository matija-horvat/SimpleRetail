using Azure.Core;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleRetail.Common.Responses;
using SimpleRetail.Common.Requests;

namespace SimpleRetail.Data.Contracts;

public interface ISupplierStoreItemRepository
{
    /// <summary>
    /// Return all members of specified entity.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="recordsToTake"></param>
    /// <returns></returns>
    Task<IEnumerable<SupplierStoreItemDto>> GetAll(int page, int recordsToTake);

    /// <summary>
    /// Return specific entity by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<SupplierStoreItemDto?> GetById(SupplierStoreItemDto request);

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
