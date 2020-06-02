using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Enums;
using CleanArchitectureExample.Domain.Resources;
using CleanArchitectureExample.Tests.Base;
using CleanArchitectureExample.Tests.BookTests;
using CleanArchitectureExample.Tests.Factories;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanArchitectureExample.Tests.BookLoanTests.Entities
{
    public class BookLoanTests
    {
        public BookLoanTests()
        {
            DomainNotificationsFacade.SetTestingEnvironment();
        }

        [Fact]
        public void ValidateBookLoan_ShouldReturnAllErrors()
        {
            //arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            BookLoan bookLoan = new BookLoan(Guid.NewGuid(), null, null);

            //act
            bookLoan.Validate();

            //assert
            baseArrangements.DomainNotifications.GetAll().Should().HaveCount(2)
                                                                  .And.Contain(x => x == Messages.BookLoan_BookIsNull)
                                                                  .And.Contain(x => x == Messages.BookLoan_TakerIsNull);

        }

        [Fact]
        public void ValidateBookLoan_Lend_ShouldReturnNoErrors()
        {
            //arrage
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            Book book = BookFactory.ReturnBook().WithBookSituation(BookSituationEnum.Awaiting);
            BookLoan bookLoan = new BookLoan(Guid.NewGuid(), book, PersonFactory.ReturnPerson());
            //act
            bookLoan.LendBook();
            //assert
            baseArrangements.DomainNotifications.GetAll().Should().BeEmpty();
        }

        [Fact]
        public void ValidateBookLoan_Lend_ShouldReturnAlreadyLent()
        {
            //arrange
            TestBaseArrangements testBaseArrangements = new TestBaseArrangements();
            Book book = BookFactory.ReturnBook().WithBookSituation(BookSituationEnum.Lent);
            BookLoan bookLoan = new BookLoan(Guid.NewGuid(), book, PersonFactory.ReturnPerson());
            
            //act
            bookLoan.LendBook();

            //assert
            testBaseArrangements.DomainNotifications.GetAll().Should().HaveCount(1)
                                                     .And.Contain(x => x == Messages.BookLoan_BookIsAlreadyLent);

        }

        [Fact]
        public void ValidateBookLoadn_Return_ShouldReturnBookNotLent()
        {
            //arrange
            TestBaseArrangements testBaseArrangements = new TestBaseArrangements();
            Book book = BookFactory.ReturnBook().WithBookSituation(BookSituationEnum.Awaiting);
            BookLoan bookLoan = new BookLoan(Guid.NewGuid(), book, PersonFactory.ReturnPerson());

            //act
            bookLoan.ReturnBook();

            //assert
            testBaseArrangements.DomainNotifications.GetAll().Should().HaveCount(1)
                                                     .And.Contain(x => x == Messages.BookLoan_BookIsNotLent);
        }

        [Fact]
        public void ValidateBookLoan_Return_ShouldReturnNoErrors()
        {
            //arrage
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            BookLoan bookLoan = new BookLoan(Guid.NewGuid(), BookFactory.ReturnBook().WithBookSituation(BookSituationEnum.Lent), PersonFactory.ReturnPerson());
            //act
            bookLoan.ReturnBook();
            //assert
            baseArrangements.DomainNotifications.GetAll().Should().BeEmpty();
            bookLoan.Book.BookSituation.Value.Should().Be(BookSituationEnum.Awaiting.Value);
        }

    }
}
