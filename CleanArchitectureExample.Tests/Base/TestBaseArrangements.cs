using CleanArchitectureExample.Domain.Core.DomainNotification;
using MediatR;
using Moq.AutoMock;

namespace CleanArchitectureExample.Tests.Base
{
    public class TestBaseArrangements
    {
        public AutoMocker Mocker { get; set; }
        public Mediator Mediator { get; set; }
        public DomainNotifications DomainNotifications { get; set; }

        public TestBaseArrangements()
        {

            Mocker = new AutoMocker();
            Mediator = Mocker.CreateInstance<Mediator>();
            DomainNotifications = Mocker.CreateInstance<DomainNotifications>();
            DomainNotificationsFacade.SetTestingEnvironment();
            DomainNotificationsFacade.SetNotificationsContainer(DomainNotifications);

        }
    }
}
