using NetTestTask.Domain.Dtos;
using NetTestTask.Dtos.Other.Generics;
using NetTestTask.Domain.Dtos.Response;

namespace NetTestTask.Application.Abstraction.Services
{
    public interface IPersonService
    {
        Task<ServiceResponse<long>> CreatePerson(string json);
        Task<ServiceResponse<string>> GetAllRequests(GetAllRequestDto model);
    }
}
