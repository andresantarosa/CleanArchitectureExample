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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotifications _domainNotifications;
        private readonly IEventDispatcher _eventDispatcher;
        protected readonly IMediator _mediator;
        public BaseController(IUnitOfWork unitOfWork, IDomainNotifications domainNotifications, IEventDispatcher eventDispatcher, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _domainNotifications = domainNotifications;
            _eventDispatcher = eventDispatcher;
            _mediator = mediator;
        }

        public async Task<IActionResult> ReturnCommand(IRequestResponse commandResponse)
        {
            // Fire pre post events
            _eventDispatcher.GetPreCommitEvents().ForEach(async x =>
            {
                await _mediator.Publish(x);
            });

            if (_domainNotifications.HasNotifications())
            {
                return Ok(new
                {
                    success = false,
                    messages = _domainNotifications.GetAll()
                });
            }
            else
            {
                if (await _unitOfWork.Commit())
                {
                    // Fire after commit events
                    _eventDispatcher.GetAfterCommitEvents().ForEach(x =>
                    {
                        _mediator.Publish(x);
                    });

                    return Ok(new
                    {
                        success = true,
                        data = commandResponse
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = false,
                        messages = new List<string>()
                        {
                            "Houve uma falha durante a gravação das informações no banco de dados"
                        }
                    });
                }
            }
        }

        public IActionResult ReturnQuery(IRequestResponse commandResponse)
        {
            if (_domainNotifications.HasNotifications())
            {
                return Ok(new
                {
                    success = false,
                    messages = _domainNotifications.GetAll()
                });
            }
            else
            {
                return Ok(new
                {
                    success = true,
                    data = commandResponse
                });
            }
        }
    }
}