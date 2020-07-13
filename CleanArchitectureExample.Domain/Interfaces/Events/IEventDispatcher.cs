using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Events
{
    public interface IEventDispatcher
    {
        List<INotification> GetPreCommitEvents();
        void AddPreCommitEvent(INotification evt);
        void RemovePreCommitEvent(INotification evt);
        Task FirePreCommitEvents();


        List<INotification> GetAfterCommitEvents();
        void AddAfterCommitEvent(INotification evt);
        void RemoveAfterCommitEvent(INotification evt);
        Task FireAfterCommitEvents();
    }
}
