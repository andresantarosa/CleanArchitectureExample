using CleanArchitectureExample.Domain.Core.Validators;
using CleanArchitectureExample.Domain.Enums;
using System;

namespace CleanArchitectureExample.Domain.Entities
{
    public class BookLoan
    {
        protected BookLoan() { }

        public BookLoan(Guid bookLoanId, Book book, Person person)
        {
            BookLoanId = bookLoanId;
            Book = book;
            Taker = person;
        }

        public Guid BookLoanId { get; private set; }
        public Guid BookId { get; private set; }
        public Guid TakerId { get; private set; }
        public DateTime CheckoutDate { get; private set; } = DateTime.Now;
        public DateTime ExpectedReturnDate { get; private set; }
        public DateTime ActualReturnDate { get; private set; }

        #region .:: Navigation Properties
        public Book Book { get; private set; }
        public Person Taker { get; private set; }
        #endregion

        public bool Validate()
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsNotNull(Book, Resources.Messages.BookLoan_BookIsNull),
                                                   AssertionsConcern.IsNotNull(Taker, Resources.Messages.BookLoan_TakerIsNull));
        }

        private bool ValidateLend()
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsEquals(Book.BookSituation.ToString(), BookSituationEnum.Lent.ToString(), Resources.Messages.BookLoan_BookSituationShouldBeLent),
                                                   AssertionsConcern.IsDateEquals(ExpectedReturnDate.Date, DateTime.Now.Date.AddDays(3), Resources.Messages.BookLoan_BookShouldBeReturnedWithin3Days));
        }
        private bool ValidateReturn()
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsEquals(Book.BookSituation.Value, BookSituationEnum.Lent.Value, Resources.Messages.BookLoan_BookIsNotLent));
        }
        private bool ValidatePreLend()
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsEquals(Book.BookSituation.Value, BookSituationEnum.Awaiting.Value, Resources.Messages.BookLoan_BookIsAlreadyLent));
        }

        public bool LendBook()
        {
            if (!ValidatePreLend())
                return false;

            ExpectedReturnDate = CheckoutDate.AddDays(3);
            Book.WithBookSituation(BookSituationEnum.Lent);
            return ValidateLend();
        }

        public bool ReturnBook()
        {
            if (!ValidateReturn())
                return false;

            ActualReturnDate = DateTime.Now;
            Book.WithBookSituation(BookSituationEnum.Awaiting);
            return true;
        }
    }
}
