using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Orchestration
{
    public class Orchestrator : IOrchestrator
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDomainNotifications _domainNotifications;
        private readonly IEventDispatcher _eventDispatcher;
        protected readonly IMediator _mediator;

        public Orchestrator(IUnitOfWork unitOfWork, IDomainNotifications domainNotifications, IEventDispatcher eventDispatcher, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _domainNotifications = domainNotifications;
            _eventDispatcher = eventDispatcher;
            _mediator = mediator;
        }

        public async Task<RequestResult> SendCommand<T>(IRequest<T> request)
        {
            var commandResponse = await _mediator.Send(request);

            // Fire pre post events
            await _eventDispatcher.FirePreCommitEvents();

            if (_domainNotifications.HasNotifications())
            {
                return new RequestResult
                {
                    Success = false,
                    Messages = _domainNotifications.GetAll()
                };
            }
            else
            {
                if (await _unitOfWork.Commit())
                {
                    // Fire after commit events
                    await _eventDispatcher.FireAfterCommitEvents();

                    return new RequestResult
                    {
                        Success = true,
                        Data = commandResponse
                    };
                }
                else
                {
                    return new RequestResult
                    {
                        Success = false,
                        Messages = new List<string>()
                        {
                            "An error ocurred while saving data"
                        }
                    };
                }
            }

        }

        public async Task<RequestResult> SendQuery<T>(IRequest<T> request)
        {
            var commandResponse = await _mediator.Send(request);

            if (_domainNotifications.HasNotifications())
            {
                return new RequestResult
                {
                    Success = false,
                    Messages = _domainNotifications.GetAll()
                };
            }
            else
            {
                return new RequestResult
                {
                    Success = true,
                    Data = commandResponse
                };
            }
        }
    }
}
