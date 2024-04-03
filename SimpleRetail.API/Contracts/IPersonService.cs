using SimpleRetail.Common.Requests;
using SimpleRetail.Common.Responses;

namespace SimpleRetail.API.Contracts;

public interface IPersonService: IService<ChangePersonRequest, PersonDto>
{
}
