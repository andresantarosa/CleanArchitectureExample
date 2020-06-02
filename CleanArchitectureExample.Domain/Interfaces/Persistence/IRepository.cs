using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence
{
    public interface IRepository<TEntity> where TEntity : class
    {
    }
}
