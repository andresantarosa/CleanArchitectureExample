using CleanArchitectureExample.Domain.Core.Validators;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Resources;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookHandlers.Commands.AddBook
{
    public static class AddBookCommandValidators
    {
        public static bool ValidateAuthorGuid(this AddBookCommand command)
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.ValidateGuidFromString(command.AuthorId, Messages.Book_AuthorGuidStringIsEmpty, Messages.Author_AuthorGuidIsEmpty, Messages.Book_AuthorGuidIsInvalid));
        }

        public static bool ValidateAuthorNotNul(this AddBookCommand command, Author author)
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsNotNull(author, Messages.Book_AuthorShouldNotBeNull));
        }
    }
}
