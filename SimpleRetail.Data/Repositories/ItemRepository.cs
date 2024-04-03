using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.Repositories;

public class ItemRepository : CrudRepository<Item, ChangeItemRequest, ItemDto>, IItemRepository
{
    public ItemRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
        : base(mapper, contextFactory)
    {
    }
}
//public class ItemRepository : IRepository<ChangeItemRequest, ItemDto>
//{
//    private readonly IMapper _mapper;
//    private readonly IDbContextFactory<DataContext> _contextFactory;

//    public ItemRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
//    {
//        _mapper = mapper;
//        _contextFactory = contextFactory;
//    }

//    public async Task<IEnumerable<ItemDto>> GetAll(int page, int recordsToTake)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var items = await newContext.Items.ToListAsync();

//        var itemsDto = _mapper.Map<List<ItemDto>>(items);

//        return itemsDto;
//    }

//    public async Task<ItemDto?> GetById(Guid id)
//    {
//        return await GetByIdInternal(id);
//    }

//    protected virtual async Task<ItemDto?> GetByIdInternal(Guid id)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var item = await newContext.Items.FirstOrDefaultAsync(u => u.Id == id);

//        var itemDto = _mapper.Map<ItemDto>(item);
//        return itemDto;
//    }

//    public async Task<ItemDto?> CreateAsync(ChangeItemRequest request)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var person = _mapper.Map<Item>(request);

//        person.InsertDate = DateTime.UtcNow;
//        person.InsertUser = request.ChangeUserId;

//        await newContext.AddAsync(person);
//        await newContext.SaveChangesAsync();

//        var result = await GetByIdInternal(person.Id);
//        return result;
//    }

//    public async Task<ItemDto?> UpdateAsync(ChangeItemRequest request)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var item = await newContext.Items.FirstOrDefaultAsync(c => c.Id == request.Id);

//        if (item is null)
//        {
//            throw new SimpleRetailException(nameof(Configuration.Messages.EntityUpdateFailedNotExists), StatusCodes.Status404NotFound);
//        }

//        item = _mapper.Map(request, item);
//        item.UpdateUser = request.ChangeUserId;
//        item.UpdateDate = DateTime.UtcNow;

//        await newContext.SaveChangesAsync();

//        return await GetByIdInternal(request.Id);
//    }

//    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var item = await newContext.Items.FirstOrDefaultAsync(c => c.Id == id);

//        if (item is null)
//        {
//            throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound);
//        }

//        item.Active = false;
//        item.UpdateUser = ChangeUserId;
//        item.UpdateDate = DateTime.UtcNow;

//        await newContext.SaveChangesAsync();

//        return;
//    }
//}
