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
        public PersonController(IUnitOfWork unitOfWork,
            IDomainNotifications domainNotifications,
            IEventDispatcher eventDispatcher,
            IMediator mediator) : base(unitOfWork, domainNotifications, eventDispatcher, mediator)
        {
        }

        [Route("Add/v1")]
        [HttpPost]
        public async Task<IActionResult> AddPerson(AddPersonCommand command)
        {
            var result = await _mediator.Send(command);
            return await ReturnCommand(result);
        }

        [Route("GetAll/v1")]
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllPeopleQuery query)
        {
            var result = await _mediator.Send(query);
            return ReturnQuery(result);
        }
    }
}