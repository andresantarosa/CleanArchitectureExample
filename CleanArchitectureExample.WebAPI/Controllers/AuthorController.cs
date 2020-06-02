using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using CleanArchitectureExample.Domain.RequestHandlers.AuthorsHandlers.Commands.AddAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : BaseController
    {
        public AuthorController(IUnitOfWork unitOfWork,
            IDomainNotifications domainNotifications,
            IEventDispatcher eventDispatcher,
            IMediator mediator) : base(unitOfWork, domainNotifications, eventDispatcher, mediator)
        {
        }

        [HttpPost]
        [Route("Add/v1")]
        public async Task<IActionResult> AddAuthor(AddAuthorCommand command)
        {
            var result = await _mediator.Send(command);
            return await ReturnCommand(result);
        }
    }
}