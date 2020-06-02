using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using CleanArchitectureExample.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Persistence.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Author> AddAuthor(Author author)
        {
            await DbSet.AddAsync(author);
            return author;
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            return await DbSet.Where(x => x.IsActive).ToListAsync();
        }

        public async Task<Author> GetAuthorById(Guid guid)
        {
            return await DbSet.FindAsync(guid);
        }
    }
}
