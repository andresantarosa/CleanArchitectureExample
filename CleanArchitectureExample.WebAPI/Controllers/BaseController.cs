using CleanArchitectureExample.Application.Orchestration;
using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using CleanArchitectureExample.Domain.Interfaces.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureExample.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {

        public async Task<IActionResult> ReturnRequestResult(RequestResult requestResult)
        {
            if (requestResult.Success)
            {
                return Ok(requestResult);
            }
            else
            {
                return BadRequest(requestResult);
            }
        }
    }
}