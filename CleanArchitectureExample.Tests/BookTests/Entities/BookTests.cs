using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Resources;
using CleanArchitectureExample.Tests.Base;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanArchitectureExample.Tests.BookTests.Entities
{
    public class BookTests: TestBaseArrangements
    {
        public BookTests():base()
        {
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("       ")]
        [InlineData(null)]
        public void ValidateBook_WithInvalidName_ShouldReturnInvalidName(string BookTitle)
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), BookTitle, 2020, 2, "432423431", new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_TitleShouldNotBeNullOrEmpty);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void ValidateBook_WithInvalidReleaseDate_ShouldReturnInvalidReleaseDate(int releaseDate)
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", releaseDate, 2, "432423431", new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_ReleaseYearShouldNotBeLowerThan1);
        }

        [Fact]
        public void ValidateBook_WithInvalidReleaseDate_ShouldReturnReleaseYearCanntoBeGreaterThanCurrentYear()
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", DateTime.Now.Year+1, 2, "432423431", new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_ReleaseYearCannotBeGraterThanCurrentYear);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void ValidateBook_WithInvalidEdition_ShouldReturnInvalidEdition(int edition)
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", DateTime.Now.Year, edition, "432423431", new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_EditionCannotBeLowerThan1);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("123123123")]
        [InlineData("1231231231233")]
        public void ValidateBook_WithReleaseDatePrior2008AndInvalidISBNLength_ShouldReturnInvalidISBNLengthForReleaseDate(string isbn)
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", 2007, 1, isbn, new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_ISBNShouldHave10Chars);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("123123123")]
        [InlineData("1231231231233")]
        public void ValidateBook_WithReleaseDatePrior2007AndInvalidISBNLength_ShouldReturnInvalidISBNLengthForReleaseDate(string isbn)
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", 2006, 1, isbn, new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_ISBNShouldHave10Chars);
        }

        [Theory]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("12345678965412")]
        [InlineData("123123123123")]
        public void ValidateBook_WithReleaseDateLatter2007AndInvalidISBNLength_ShouldReturnInvalidISBNLengthForReleaseDate(string isbn)
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", 2009, 1, isbn, new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_ISBNShouldHave13Chars);
        }

        [Fact]
        public void ValidateBook_WithInvalidAuthor_ShouldReturnInvalidAuthor()
        {
            //Arrange
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", 2009, 1, "1234567896541", null);

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_AuthorShouldNotBeNull);
        }
    }
}
