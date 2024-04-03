using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SimpleRetail.Common;
using SimpleRetail.Common.Errors;
using SimpleRetail.Common.Requests;
using SimpleRetail.Data.Contracts;
using SimpleRetail.Common.Responses;
using SimpleRetail.Data.EF;
using SimpleRetail.Data.EF.Model;

namespace SimpleRetail.Data.Repositories;

public class PersonRepository : CrudRepository<Person, ChangePersonRequest, PersonDto>, IPersonRepository
{
    public PersonRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
        : base(mapper, contextFactory)
    {
    }
}
//public class PersonRepository : IRepository<ChangePersonRequest, PersonDto>
//{
//    private readonly IMapper _mapper;
//    private readonly IDbContextFactory<DataContext> _contextFactory;

//    public PersonRepository(IMapper mapper, IDbContextFactory<DataContext> contextFactory)
//    {
//        _mapper = mapper;
//        _contextFactory = contextFactory;
//    }

//    public async Task<IEnumerable<PersonDto>> GetAll(int page, int recordsToTake)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var persons = await newContext.Persons.ToListAsync();

//        var personsDto = _mapper.Map<List<PersonDto>>(persons);

//        return personsDto;
//    }

//    public async Task<PersonDto?> GetById(Guid id)
//    {
//        return await GetByIdInternal(id);
//    }

//    protected virtual async Task<PersonDto?> GetByIdInternal(Guid id)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var person = await newContext.Persons.FirstOrDefaultAsync(u => u.Id == id);

//        var personDto = _mapper.Map<PersonDto>(person);
//        return personDto;
//    }

//    public async Task<PersonDto?> CreateAsync(ChangePersonRequest request)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var person = _mapper.Map<Person>(request);

//        person.InsertDate = DateTime.UtcNow;
//        person.InsertUser = request.ChangeUserId;

//        await newContext.AddAsync(person);
//        await newContext.SaveChangesAsync();

//        var result = await GetByIdInternal(person.Id);
//        return result;
//    }

//    public async Task<PersonDto?> UpdateAsync(ChangePersonRequest request)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var person = await newContext.Persons.FirstOrDefaultAsync(c => c.Id == request.Id);

//        if (person is null)
//        {
//            throw new SimpleRetailException(nameof(Configuration.Messages.EntityUpdateFailedNotExists), StatusCodes.Status404NotFound);
//        }

//        person = _mapper.Map(request, person);
//        person.UpdateUser = request.ChangeUserId;
//        person.UpdateDate = DateTime.UtcNow;

//        await newContext.SaveChangesAsync();

//        return await GetByIdInternal(request.Id);
//    }

//    public async Task DeleteAsync(Guid id, Guid ChangeUserId)
//    {
//        using var newContext = await _contextFactory.CreateDbContextAsync();

//        var person = await newContext.Persons.FirstOrDefaultAsync(c => c.Id == id);

//        if (person is null)
//        {
//            throw new SimpleRetailException(nameof(Configuration.Messages.EntityDeleteFailedNotExists), StatusCodes.Status404NotFound);
//        }

//        person.Active = false;
//        person.UpdateUser = ChangeUserId;
//        person.UpdateDate = DateTime.UtcNow;

//        await newContext.SaveChangesAsync();

//        return;
//    }
//}
