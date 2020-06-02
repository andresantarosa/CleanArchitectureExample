using CleanArchitectureExample.Domain.Interfaces.Requests;
using System;

namespace CleanArchitectureExample.Domain.RequestHandlers.AuthorsHandlers.Commands.AddAuthor
{
    public class AddAuthorCommandResponseViewModel: IRequestResponse
    {
        public Guid AuthorId { get; set; }
        public string Name { get; set; }
    }
}
