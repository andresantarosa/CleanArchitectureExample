using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        public Task<bool> Commit();
    }
}
