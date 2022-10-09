using NetTestTask.Domain.Dtos;
using NetTestTask.Dtos.Other.Generics;
using NetTestTask.Domain.Dtos.Response;
using NetTestTask.Application.Abstraction.Services;
using NetTestTask.Application.Abstraction.Presentation;

namespace NetTestTask.Services.Implementation.Presentation
{
    public class PersonControllerService : IPersonControllerService
    {
        private readonly IPersonService _personService;
        public PersonControllerService(IPersonService personService)
        {
            _personService = personService;
        }
        public async Task<ServiceResponse<long>> Create(string json)
        {
            return await _personService.CreatePerson(json);
        }

        public async Task<ServiceResponse<string>> GetAllRequests(GetAllRequestDto model)
        {
            return await _personService.GetAllRequests(model);
        }
    }
}
