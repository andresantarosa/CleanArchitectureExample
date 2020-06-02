using CleanArchitectureExample.Domain.Core.Validators;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Resources;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan
{
    public static class RequestLoanCommandValidator
    {
        public static bool ValidatePersonNotNul(this RequestLoanCommand command, Person person)
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsNotNull(person, Messages.BookLoan_PersonNotFound));
        }

        public static bool ValidateBookGuid(this RequestLoanCommand command)
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.ValidateGuidFromString(command.BookId, Messages.BookLoan_BookGuidStringIsEmpty, Messages.BookLoan_BookGuidIsEmpty, Messages.BookLoan_BookGuidIsInvalid));
        }

        public static bool ValidateBookNotNul(this RequestLoanCommand command, Book book)
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsNotNull(book, Messages.BookLoan_BookNotFound));
        }
    }
}
