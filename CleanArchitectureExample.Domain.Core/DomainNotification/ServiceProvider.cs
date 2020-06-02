using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Core.DomainNotification
{
    public interface IContainer
    {
        T GetService<T>(Type type);
    }
}
