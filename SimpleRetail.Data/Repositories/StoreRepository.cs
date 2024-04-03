using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common.Requests;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.Repositories;

public class StoreRepository: CrudRepository<Store, ChangeStoreRequest, StoreDto>, IStoreRepository
{
    public StoreRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
        : base(mapper, contextFactory)
    {
    }

    public async Task<StoreDto?> GetDetailsById(Guid id)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var store = await newContext.Stores
                                        .Include(c => c.Contact)
                                        .FirstOrDefaultAsync(u => u.Id == id);

        var storeDto = _mapper.Map<StoreDto>(store);
        return storeDto;
    }
}
