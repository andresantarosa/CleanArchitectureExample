using CleanArchitectureExample.Application.Orchestration;
using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Commands.AddPerson;
using CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        private readonly IOrquestrator _orquestrator;

        public PersonController(IOrquestrator orquestrator)
        {
            _orquestrator = orquestrator;
        }

        [Route("Add/v1")]
        [HttpPost]
        public async Task<IActionResult> AddPerson(AddPersonCommand command)
        {
            RequestResult requestResult = await _orquestrator.SendCommand(command);
            return await ReturnRequestResult(requestResult);
        }

        [Route("GetAll/v1")]
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllPeopleQuery query)
        {
            var result = await _orquestrator.SendQuery(query);
            return await ReturnRequestResult(result);
        }
    }
}