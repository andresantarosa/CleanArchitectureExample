using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookHandlers.Commands.AddBook
{
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, AddBookCommandResponseViewModel>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public AddBookCommandHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<AddBookCommandResponseViewModel> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            AddBookCommandResponseViewModel result = new AddBookCommandResponseViewModel();

            if (!request.ValidateAuthorGuid())
                return result;

            Author author = await _authorRepository.GetAuthorById(Guid.Parse(request.AuthorId));
            if (!request.ValidateAuthorNotNul(author))
                return result;

            Book book = new Book(Guid.NewGuid(), request.Title, request.ReleaseYear, request.Edition, request.ISBN, author);
            if (!book.Validate())
                return result;

            await _bookRepository.AddBook(book);

            result.BookId = book.BookId;
            result.Title = book.Title;
            result.ReleaseYear = book.ReleaseYear;
            result.Edition = book.Edition;
            result.ISBN = book.ISBN;
            result.AuthorName = book.Author.Name;

            return result;
        }
    }
}
