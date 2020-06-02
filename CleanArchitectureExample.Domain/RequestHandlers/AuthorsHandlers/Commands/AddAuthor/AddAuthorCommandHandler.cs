using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.RequestHandlers.AuthorsHandlers.Commands.AddAuthor
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, AddAuthorCommandResponseViewModel>
    {
        private readonly IAuthorRepository _authorRepository;

        public AddAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AddAuthorCommandResponseViewModel> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            AddAuthorCommandResponseViewModel response = new AddAuthorCommandResponseViewModel();

            Author author = new Author(Guid.NewGuid(), request.Name);
            if (!author.Validate())
                return response;

            await _authorRepository.AddAuthor(author);

            response.AuthorId = author.AuthorId;
            response.Name = author.Name;

            return response;
        }
    }
}
