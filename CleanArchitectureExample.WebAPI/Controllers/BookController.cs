using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using CleanArchitectureExample.Domain.RequestHandlers.BookHandlers.Commands.AddBook;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        public BookController(IUnitOfWork unitOfWork,
            IDomainNotifications domainNotifications,
            IEventDispatcher eventDispatcher,
            IMediator mediator) : base(unitOfWork, domainNotifications, eventDispatcher, mediator)
        {
        }

        [Route("Add/v1")]
        [HttpPost]
        public async Task<IActionResult> AddBook(AddBookCommand command)
        {
            AddBookCommandResponseViewModel result = await _mediator.Send(command);
            return await ReturnCommand(result);
        }
    }
}