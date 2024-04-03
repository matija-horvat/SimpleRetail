using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common.Errors;
using SimpleRetail.Common;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.Repositories;

public class SupplierStoreItemRepository : ISupplierStoreItemRepository
{
    private readonly IMapper _mapper;
    private readonly IDbContextFactory<DataContext> _contextFactory;

    public SupplierStoreItemRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _mapper = mapper;
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<SupplierStoreItemDto>> GetAll(int page, int recordsToTake)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var supplierItems = await newContext.SupplierItems.ToListAsync();

        var supplierItemDtos = _mapper.Map<List<SupplierStoreItemDto>>(supplierItems);

        return supplierItemDtos;
    }



    public async Task<SupplierStoreItemDto?> GetById(SupplierStoreItemDto request)
    {
        return await GetByIdInternal(request);
    }

    protected virtual async Task<SupplierStoreItemDto?> GetByIdInternal(SupplierStoreItemDto request)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var supplierItem = await newContext.SupplierItems.FirstOrDefaultAsync(u => u.SupplierId == request.SupplierId && u.ItemId == request.ItemId && u.StoreId == request.StoreId);

        var supplierItemDto = _mapper.Map<SupplierStoreItemDto>(supplierItem);
        return supplierItemDto;
    }

    public async Task<SupplierStoreItemDto?> CreateAsync(SupplierStoreItemDto request)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var supplierItem = _mapper.Map<SupplierItem>(request);

        await newContext.AddAsync(supplierItem);
        await newContext.SaveChangesAsync();

        var result = await GetByIdInternal(request);
        return result;
    }

    public async Task DeleteAsync(SupplierStoreItemDeleteRequest request)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var entity = await newContext.SupplierItems.FirstOrDefaultAsync(u => u.SupplierId == request.SupplierId && u.ItemId == request.ItemId && u.StoreId == request.StoreId);

        if (entity is null)
        {
            throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound);
        }

        newContext.SupplierItems.Remove(entity);
        await newContext.SaveChangesAsync();
    }
}
