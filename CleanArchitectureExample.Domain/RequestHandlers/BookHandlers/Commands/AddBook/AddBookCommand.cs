using MediatR;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookHandlers.Commands.AddBook
{
    public class AddBookCommand : IRequest<AddBookCommandResponseViewModel>
    {
        public AddBookCommand(string title, int releaseYear, int edition, string iSBN, string authorId)
        {
            Title = title;
            ReleaseYear = releaseYear;
            Edition = edition;
            ISBN = iSBN;
            AuthorId = authorId;
        }

        public string Title { get; private set; }
        public int ReleaseYear { get; private set; }
        public int Edition { get; private set; }
        public string ISBN { get; private set; }
        public string AuthorId { get; private set; }
    }
}
