using CleanArchitectureExample.Application.Orchestration;
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
        private readonly IOrquestrator _orquestrator;

        public AuthorController(IOrquestrator orquestrator)
        {
            _orquestrator = orquestrator;
        }

        [HttpPost]
        [Route("Add/v1")]
        public async Task<IActionResult> AddAuthor(AddAuthorCommand command)
        {
            RequestResult requestResult = await _orquestrator.SendCommand(command);
            return await ReturnRequestResult(requestResult);
        }
    }
}