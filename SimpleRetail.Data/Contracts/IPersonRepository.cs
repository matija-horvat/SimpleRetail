using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.Data.Contracts;

public interface IPersonRepository : IRepository<ChangePersonRequest, PersonDto>
{
}
