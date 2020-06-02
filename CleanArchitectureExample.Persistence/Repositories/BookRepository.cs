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
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Book> AddBook(Book book)
        {
            await DbSet.AddAsync(book);
            return book;
        }

        public async Task<List<Book>> GetAllBook()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<Book> GetBookByISBN(string ISBN)
        {
            return await DbSet.FirstOrDefaultAsync(b => b.ISBN == ISBN);
        }

        public async Task<Book> GetById(Guid Id)
        {
            return await DbSet.FindAsync(Id);
        }

        public async Task<List<Book>> GetByTitle(string title)
        {
            return await DbSet.Where(b => b.Title.Contains(title)).ToListAsync();
        }
    }
}
