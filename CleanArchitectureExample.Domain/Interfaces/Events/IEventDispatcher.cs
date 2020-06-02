using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Interfaces.Events
{
    public interface IEventDispatcher
    {
        List<INotification> GetAfterCommitEvents();
        void AddAfterCommitEvent(INotification evt);
        void RemoveAfterCommitEvent(INotification evt);


        List<INotification> GetPreCommitEvents();
        void AddPreCommitEvent(INotification evt);
        void RemovePreCommitEvent(INotification evt);
    }
}
