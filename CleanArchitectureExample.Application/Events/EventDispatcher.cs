using CleanArchitectureExample.Domain.Interfaces.Events;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Events
{
    public class EventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;

        public EventDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }


        private List<INotification> AfterCommitEvents { get; set; } = new List<INotification>();
        private List<INotification> PreCommitEvents { get; set; } = new List<INotification>();


        public void AddAfterCommitEvent(INotification evt)
        {
            AfterCommitEvents.Add(evt);
        }

        public void AddPreCommitEvent(INotification evt)
        {
            PreCommitEvents.Add(evt);

        }

        public async Task FireAfterCommitEvents()
        {
            AfterCommitEvents.ForEach(async x =>
            {
                await _mediator.Publish(x);
            });
        }

        public async Task FirePreCommitEvents()
        {
            PreCommitEvents.ForEach(async x =>
            {
                await _mediator.Publish(x);
            });
        }

        public List<INotification> GetAfterCommitEvents()
        {
            return AfterCommitEvents;
        }

        public List<INotification> GetPreCommitEvents()
        {
            return PreCommitEvents;
        }



        public void RemoveAfterCommitEvent(INotification evt)
        {
            AfterCommitEvents.Remove(evt);
        }

        public void RemovePreCommitEvent(INotification evt)
        {
            PreCommitEvents.Remove(evt);
        }
    }
}
