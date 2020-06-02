using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan;
using CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.ReturnBook;
using CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookLoanController : BaseController
    {
        public BookLoanController(IUnitOfWork unitOfWork,
            IDomainNotifications domainNotifications,
            IEventDispatcher eventDispatcher,
            IMediator mediator) : base(unitOfWork, domainNotifications, eventDispatcher, mediator)
        {
        }

        [HttpPost]
        [Route("RequestLoan/v1")]
        public async Task<IActionResult> Loan(RequestLoanCommand command)
        {
            var result = await _mediator.Send(command, new System.Threading.CancellationToken());
            return await ReturnCommand(result);
        }

        [HttpPost]
        [Route("ReturnBook/v1")]
        public async Task<IActionResult> ReturnBook(ReturnBookCommand command)
        {
            var result = await _mediator.Send(command, new System.Threading.CancellationToken());
            return await ReturnCommand(result);
        }

        [HttpGet]
        [Route("GetAll/v1")]
        public async Task<IActionResult> GetAllLoans(BookLoanGetAllQuery query)
        {
            var result = await _mediator.Send(query, new System.Threading.CancellationToken());
            return ReturnQuery(result);
        }
    }
}