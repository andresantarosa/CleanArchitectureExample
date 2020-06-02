using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Core.DomainNotification
{
    public interface IDomainNotifications
    {
        void AddNotification(string notification);
        bool HasNotifications();
        List<string> GetAll();
        void CleanNotifications();
    }
}
