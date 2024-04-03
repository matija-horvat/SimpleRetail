using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common;
using SimpleRetail.Common.Errors;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.Utils;

namespace SimpleRetail.Data.Repositories;

public class CrudRepository<TEntity, TRequest, TResponse> : IRepository<TRequest, TResponse>
    where TEntity : class
    where TRequest : class
    where TResponse : class
{
    protected readonly IMapper _mapper;
    protected readonly IDbContextFactory<DataContext> _contextFactory;

    public CrudRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
    {
        _mapper = mapper;
        _contextFactory = contextFactory;
    }

    public async Task<IEnumerable<TResponse>> GetAll(int page, int recordsToTake)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var entities = await newContext.Set<TEntity>().Paginate(page, recordsToTake).ToListAsync();

        var dtos = _mapper.Map<List<TResponse>>(entities);

        return dtos;
    }

    public async Task<TResponse?> GetById(Guid id)
    {
        return await GetByIdInternal(id);
    }

    protected virtual async Task<TResponse?> GetByIdInternal(Guid id)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var entity = await newContext.Set<TEntity>().FindAsync(id);

        var dto = _mapper.Map<TResponse>(entity);
        return dto;
    }

    public async Task<TResponse?> CreateAsync(TRequest request)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var entity = _mapper.Map<TEntity>(request);

        var insertProperties = entity.GetType().GetProperties().Where(p => p.Name.StartsWith("Insert")).ToList();
        foreach (var property in insertProperties)
        {
            if(property.Name.Equals("InsertDate"))
                property.SetValue(entity, DateTime.UtcNow);
            if (property.Name.Equals("InsertUser"))
                property.SetValue(entity, request.GetType().GetProperty("ChangeUserId")?.GetValue(request));
        }

        await newContext.Set<TEntity>().AddAsync(entity);
        await newContext.SaveChangesAsync();

        var result = await GetByIdInternal((Guid)entity.GetType().GetProperty("Id").GetValue(entity));
        return result;
    }

    public async Task<TResponse?> UpdateAsync(TRequest request)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var entity = await newContext.Set<TEntity>().FindAsync(request.GetType().GetProperty("Id")?.GetValue(request));

        if (entity is null)
        {
            throw new SimpleRetailException(nameof(Configuration.Messages.EntityUpdateFailedNotExists), StatusCodes.Status404NotFound);
        }

        entity = _mapper.Map(request, entity);

        var updateProperties = entity.GetType().GetProperties().Where(p => p.Name.StartsWith("Update")).ToList();
        foreach (var property in updateProperties)
        {
            if (property.Name.Equals("UpdateDate"))
                property.SetValue(entity, DateTime.UtcNow);
            if (property.Name.Equals("UpdateUser"))
                property.SetValue(entity, request.GetType().GetProperty("ChangeUserId")?.GetValue(request));
        }

        await newContext.SaveChangesAsync();

        return await GetByIdInternal((Guid)entity.GetType().GetProperty("Id").GetValue(entity));
    }

    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
    {
        using var newContext = await _contextFactory.CreateDbContextAsync();

        var entity = await newContext.Set<TEntity>().FindAsync(id);

        if (entity is null)
        {
            throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound);
        }

        entity.GetType().GetProperty("Active")?.SetValue(entity, false);
        entity.GetType().GetProperty("UpdateUser")?.SetValue(entity, ChangeUserId);
        entity.GetType().GetProperty("UpdateDate")?.SetValue(entity, DateTime.UtcNow);

        await newContext.SaveChangesAsync();
    }


}
