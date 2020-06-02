using CleanArchitectureExample.Domain.Interfaces.Persistence;
using CleanArchitectureExample.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Persistence.Repositories.Base
{
    public class BaseRepositoryReadOnly<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContextReadOnly Context;
        protected DbSet<TEntity> DbSet;

        public BaseRepositoryReadOnly(ApplicationDbContextReadOnly context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
    }
}
