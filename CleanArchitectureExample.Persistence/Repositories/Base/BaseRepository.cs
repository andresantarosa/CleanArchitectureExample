using CleanArchitectureExample.Domain.Interfaces.Persistence;
using CleanArchitectureExample.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitectureExample.Persistence.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext Context;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
    }

   
}
