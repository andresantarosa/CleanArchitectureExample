using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Orchestration
{
    public interface IOrchestrator
    {
        Task<RequestResult> SendCommand<T>(IRequest<T> request);
        Task<RequestResult> SendQuery<T>(IRequest<T> request);
    }
}
