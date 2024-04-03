using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.Repositories;

public class SupplierRepository : CrudRepository<Supplier, ChangeSupplierRequest, SupplierDto>, ISupplierRepository
{
    public SupplierRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
        : base(mapper, contextFactory)
    {
    }

    public async Task<SupplierDto?> GetDetailsById(Guid id)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var supplier = await newContext.Suppliers
                                        .Include(c => c.Contact)
                                        .FirstOrDefaultAsync(u => u.Id == id);

        var supplierDto = _mapper.Map<SupplierDto>(supplier);
        return supplierDto;
    }
}
