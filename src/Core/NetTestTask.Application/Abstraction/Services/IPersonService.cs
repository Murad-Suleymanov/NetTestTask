using NetTestTask.Domain.Dtos;
using NetTestTask.Dtos.Other.Generics;
using NetTestTask.Domain.Dtos.Response;

namespace NetTestTask.Application.Abstraction.Services
{
    public interface IPersonService
    {
        ServiceResponse<GenericAddingDto> CreatePerson(PersonDto model);
    }
}
