using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories
{
    public interface IBookRepository
    {
        Task<Book> AddBook(Book book);
        Task<Book> GetBookByISBN(string ISBN);
        Task<List<Book>> GetAllBook();
        Task<List<Book>> GetByTitle(string title);
        Task<Book> GetById(Guid Id);
    }
}
