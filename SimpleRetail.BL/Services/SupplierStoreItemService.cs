using SimpleRetail.BL.Contracts;
using SimpleRetail.Common.Errors;
using SimpleRetail.Common;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;

namespace SimpleRetail.BL.Services;

public class SupplierStoreItemService : ISupplierStoreItemService
{
    private readonly ISupplierStoreItemRepository _supplierStoreItemRepository;
    private readonly ISupplierService _supplierService;
    private readonly IStoreService _storeService;
    private readonly IItemService _itemService;

    public SupplierStoreItemService(ISupplierStoreItemRepository supplierStoreItemRepository, ISupplierService supplierService, IStoreService storeService, IItemService itemService)
    {
        _supplierStoreItemRepository = supplierStoreItemRepository;
        _supplierService = supplierService;
        _storeService = storeService;
        _itemService = itemService;
    }

    public async Task<IEnumerable<SupplierStoreItemDto>> GetAll(int page, int recordsToTake)
    {
        return await _supplierStoreItemRepository.GetAll(page, recordsToTake);
    }

    public async Task<SupplierStoreItemDto?> CreateAsync(SupplierStoreItemDto request)
    {
        var entity = await _supplierStoreItemRepository.GetById(request);
        if (entity != null) { throw new SimpleRetailException(nameof(Configuration.Messages.EntityCreateFailedAlreadyExists), StatusCodes.Status404NotFound); }

        var result = await _supplierStoreItemRepository.CreateAsync(request);
        return result;
    }

    public async Task DeleteAsync(SupplierStoreItemDeleteRequest request)
    {
        var supplier = await _supplierService.GetById(request.SupplierId);
        if (supplier == null) { throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound); }

        var store = await _storeService.GetById(request.StoreId);
        if (store == null) { throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound); }

        var item = await _itemService.GetById(request.ItemId);
        if (item == null) { throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound); }

        await _supplierStoreItemRepository.DeleteAsync(request);
        return;
    }
}
