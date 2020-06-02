using CleanArchitectureExample.Domain.Core.Validators;
using CleanArchitectureExample.Domain.Enums;
using CleanArchitectureExample.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Book
    {
        protected Book() { }

        public Book(Guid bookId, string title, int releaseYear, int edition, string iSBN, Author author)
        {
            BookId = bookId;
            Title = title;
            ReleaseYear = releaseYear;
            Edition = edition;
            ISBN = iSBN;
            Author = author;
        }

        public Guid BookId { get; private set; }
        public string Title { get; private set; }
        public int ReleaseYear { get; private set; }
        public int Edition { get; private set; }
        public string ISBN { get; private set; }
        public Guid AuthorId { get; private set; }
        public BookSituationEnum BookSituation { get; private set; } = BookSituationEnum.Awaiting;

        #region .:: Navigation Properties ::.
        public virtual Author Author { get; private set; }
        public virtual ICollection<BookLoan> BookLoans { get; private set; }
        #endregion

        public bool Validate()
        {
            bool generalValidations = AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsStringNotNullOrWhiteSpace(Title, Messages.Book_TitleShouldNotBeNullOrEmpty),
                                                                      AssertionsConcern.IsGreaterThanOrEquals(ReleaseYear, 1, Messages.Book_ReleaseYearShouldNotBeLowerThan1),
                                                                      AssertionsConcern.IsLowerOrEquals(ReleaseYear, DateTime.Now.Year, Messages.Book_ReleaseYearCannotBeGraterThanCurrentYear),
                                                                      AssertionsConcern.IsGreaterThan(Edition, 0, Messages.Book_EditionCannotBeLowerThan1),
                                                                      AssertionsConcern.IsNotNull(Author, Messages.Book_AuthorShouldNotBeNull ));
            bool ISBNValidations = ValidateISBN();

            return generalValidations && ISBNValidations;
        }

        private bool ValidateISBN()
        {
            if(ReleaseYear <= 2007)
                return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.HasLengthEquals(ISBN, 10, Messages.Book_ISBNShouldHave10Chars));
            else
                return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.HasLengthEquals(ISBN, 13, Messages.Book_ISBNShouldHave13Chars));
        }

        public Book WithBookSituation(BookSituationEnum bookSituation)
        {
            this.BookSituation = bookSituation;
            return this;
        }
    }
}
