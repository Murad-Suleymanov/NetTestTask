using NetTestTask.Application.Abstraction.Services;
using NetTestTask.Common.CustomExceptions;
using NetTestTask.Common.ResourceValues;
using NetTestTask.DataAccess.Abstractions;
using NetTestTask.DataAccess.Abstractions.Repositories;
using NetTestTask.Domain.Daos.Main;
using NetTestTask.Domain.Dtos;
using NetTestTask.Domain.Dtos.Response;
using NetTestTask.Utility.Helpers;

namespace NetTestTask.Services.Implementation.Services
{
    public class PersonService : IPersonService
    {
        CustomJsonSerializer _jsonSerializer;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _jsonSerializer = new CustomJsonSerializer(typeof(Person));
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResponse<long>> CreatePerson(string json)
        {
            var person = _jsonSerializer.Deserialize(json) as Person;
            await _personRepository.AddWithCommitAsync(person);

            return new ServiceResponse<long>
            {
                Data = person.Id,
            };
        }

        public async Task<ServiceResponse<string>> GetAllRequests(GetAllRequestDto model)
        {
            var data = await _personRepository.GetAllAsync(x => (!string.IsNullOrWhiteSpace(model.FirstName) ? x.FirstName == model.FirstName : true) &&
                                                                    (!string.IsNullOrWhiteSpace(model.LastName) ? x.LastName == model.LastName : true) &&
                                                                    (!string.IsNullOrWhiteSpace(model.City) ? x.Address.City == model.City : true));


            if (data is null || !data.Any())
                throw new NotFoundException("IE400", NotificationValues.DataNotFound);

            return new ServiceResponse<string>
            {
                Data = _jsonSerializer.Serialize(data)
            };
        }
    }
}
