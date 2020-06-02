using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Core.DomainNotification
{
    public static class DomainNotificationsFacade
    {
        [ThreadStatic]
        private static IDomainNotifications _mockContainer;
        private static bool _fromTesting;

        public static void SetTestingEnvironment()
        {
            _fromTesting = true;
        }

        /// <summary>
        /// This method should be used only for testing purpose
        /// Under normal use the container is obtained via DI
        /// </summary>
        /// <param name="mockContainer"></param>
        public static void SetNotificationsContainer(IDomainNotifications mockContainer)
        {
            if (_fromTesting == false)
                throw new Exception(@"For SetNotificationsContainer to work properly SetTestingEnvironment() should be called first. 
                                      This method should be used only for testing purpose");
            _mockContainer = mockContainer;
        }

        private static IDomainNotifications GetContainer()
        {
            if (_fromTesting)
                return _mockContainer;
            else
                return ServiceLocator.Container?.GetService<IDomainNotifications>(typeof(IDomainNotifications));
        }

        public static void AddNotification(string notification)
        {
            var container = GetContainer();
            container.AddNotification(notification);
        }

        public static List<string> GetAll()
        {
            return GetContainer().GetAll();
        }

        public static bool HasNotifications()
        {
            return GetContainer().HasNotifications();
        }
    }
}
