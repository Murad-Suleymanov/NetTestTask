using Microsoft.AspNetCore.Mvc;
using NetTestTask.Application.Abstraction.Presentation;
using NetTestTask.Domain.Dtos;

namespace NetTestTask.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonControllerService _personControllerService;
        public PersonController(IPersonControllerService personControllerService)
        {
            _personControllerService = personControllerService;
        }
        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Post(string person)
        {
           var id =  await _personControllerService.Create(person);

            return Ok(id);
        }
        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(GetAllRequestDto request)
        {
            var persons = await _personControllerService.GetAllRequests(request);
            return Ok(persons);
        }
    }
}
