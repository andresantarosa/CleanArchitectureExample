using CleanArchitectureExample.Domain.Core.DomainNotification;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Core.DomainNotification
{
    public static class ServiceLocator
    {
        public static IContainer Container { get; private set; }

        public static void Initialize(IContainer container)
        {
            Container = container;
        }
    }
}
