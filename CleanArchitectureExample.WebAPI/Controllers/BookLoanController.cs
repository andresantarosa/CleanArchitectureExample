using CleanArchitectureExample.Application.Orchestration;
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
        private readonly IOrquestrator _orquestrator;

        public BookLoanController(IOrquestrator orquestrator)
        {
            _orquestrator = orquestrator;
        }

        [HttpPost]
        [Route("RequestLoan/v1")]
        public async Task<IActionResult> Loan(RequestLoanCommand command)
        {
            RequestResult requestResult = await _orquestrator.SendCommand(command);
            return await ReturnRequestResult(requestResult);
        }

        [HttpPost]
        [Route("ReturnBook/v1")]
        public async Task<IActionResult> ReturnBook(ReturnBookCommand command)
        {
            RequestResult requestResult = await _orquestrator.SendCommand(command);
            return await ReturnRequestResult(requestResult);
        }

        [HttpGet]
        [Route("GetAll/v1")]
        public async Task<IActionResult> GetAllLoans(BookLoanGetAllQuery query)
        {
            RequestResult requestResult = await _orquestrator.SendQuery(query);
            return await ReturnRequestResult(requestResult);
        }
    }
}