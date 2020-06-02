using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories
{
    public interface IAuthorRepository
    {
        public Task<Author> AddAuthor(Author author);
        public Task<List<Author>> GetAllAuthors();
        public Task<Author> GetAuthorById(Guid guid);

    }
}
