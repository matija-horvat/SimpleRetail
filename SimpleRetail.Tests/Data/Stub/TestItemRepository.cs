using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.Repositories;
using SimpleRetail.Tests.Data.Dtos;

namespace SimpleRetail.Tests.Data.Stub;

public class TestItemRepository : ItemRepository
{
    public TestItemRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory) : base(mapper, contextFactory)
    {

    }
    protected override async Task<ItemDto?> GetByIdInternal(Guid id)
    {
        return await Task.FromResult(ItemDtoMock.Get()).ConfigureAwait(false);
    }
}
