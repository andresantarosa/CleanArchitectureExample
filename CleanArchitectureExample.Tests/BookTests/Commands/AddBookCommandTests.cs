using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using CleanArchitectureExample.Domain.RequestHandlers.BookHandlers.Commands.AddBook;
using CleanArchitectureExample.Domain.Resources;
using CleanArchitectureExample.Tests.Base;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitectureExample.Tests.BookTests.Commands
{
    public class AddBookCommandTests
    {
        public AddBookCommandTests()
        {
            DomainNotificationsFacade.SetTestingEnvironment();
        }

        [Theory]
        [InlineData(" ", "Author Guid string is empty")]
        [InlineData("00000000-0000-0000-0000-000000000000", "Author Guid is empty")]
        public async void HandleAddBookCommand_WithInvalidGuid_ShouldReturnInvalidAuthorGuid(string authorId, string outputError)
        {
            //Arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            AddBookCommand addBookCommand = new AddBookCommand("Pet Sematary", 2013, 1, "9788581050393", authorId);
            var sut = baseArrangements.Mocker.CreateInstance<AddBookCommandHandler>();

            //Act
            AddBookCommandResponseViewModel result = await sut.Handle(addBookCommand, new CancellationToken());

            //Assert

            baseArrangements.DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                          .And.Contain(x => x == outputError);
            baseArrangements.Mocker.GetMock<IAuthorRepository>().Verify(x => x.GetAuthorById(It.IsAny<Guid>()), Times.Never());

        }


        //Test to assure the Validate method from Book domain entity was called
        [Fact]
        public void HandleAddBookCommand_WithInvalidName_ShouldReturnInvalidName()
        {
            //Arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            Book book = new Book(Guid.NewGuid(), "   ", 2020, 2, "432423431", new Author(Guid.NewGuid(), "Stephen King"));

            //Act
            bool isValid = book.Validate();

            //Assert
            isValid.Should().BeFalse();
            baseArrangements.DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                                  .And.Contain(x => x == Messages.Book_TitleShouldNotBeNullOrEmpty);
            baseArrangements.Mocker.GetMock<IBookRepository>().Verify(x => x.AddBook(It.IsAny<Book>()), Times.Never());

        }

        [Fact]
        public async void HandleAddBookCommand_WitInvalidAuthor_ShouldReturnInvalidAuthor()
        {
            //Arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            AddBookCommand addBookCommand = new AddBookCommand("Pet Sematary", 2013, 1, "9788581050393", Guid.NewGuid().ToString());
            var sut = baseArrangements.Mocker.CreateInstance<AddBookCommandHandler>();


            baseArrangements.Mocker.GetMock<IAuthorRepository>()
                                   .Setup(b => b.GetAuthorById(It.IsAny<Guid>()))
                                   .Returns(() => Task.FromResult<Author>(null));

            //Act
            AddBookCommandResponseViewModel result = await sut.Handle(addBookCommand, new CancellationToken());

            //Assert
            baseArrangements.Mocker.GetMock<IAuthorRepository>().Verify(x => x.GetAuthorById(It.IsAny<Guid>()), Times.Once());
            baseArrangements.Mocker.GetMock<IBookRepository>().Verify(x => x.AddBook(It.IsAny<Book>()), Times.Never());
            baseArrangements.DomainNotifications.GetAll().Should().NotBeEmpty()
                                                .And.Contain(x => x == Messages.Book_AuthorShouldNotBeNull);
        }

        [Fact]
        public async void HandleAddBookCommand_WithCorrectParameters_ShouldReturnNoErrors()
        {
            //Arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();

            #region .:: Objects build ::.
            AddBookCommand addBookCommand = new AddBookCommand("Pet Sematary", 2013, 1, "9788581050393", Guid.NewGuid().ToString());
            Author author = new Author(Guid.NewGuid(), "Stephen King");
            Book book = new Book(Guid.NewGuid(), "Pet Sematary", 2013, 1, "9788581050393", author);
            #endregion

            #region .:: Mocks ::.
            baseArrangements.Mocker.GetMock<IAuthorRepository>()
                                   .Setup(b => b.GetAuthorById(It.IsAny<Guid>()))
                                   .Returns(() => Task.FromResult<Author>(author));

            baseArrangements.Mocker.GetMock<IBookRepository>()
                                   .Setup(b => b.AddBook(It.IsAny<Book>()))
                                   .Returns(() => Task.FromResult(book));
            #endregion

            var sut = baseArrangements.Mocker.CreateInstance<AddBookCommandHandler>();

            //Act
            AddBookCommandResponseViewModel result = await sut.Handle(addBookCommand, new CancellationToken());

            //Assert
            baseArrangements.DomainNotifications.GetAll().Should().BeEmpty();
            baseArrangements.Mocker.GetMock<IAuthorRepository>().Verify(x => x.GetAuthorById(It.IsAny<Guid>()), Times.Once());
            baseArrangements.Mocker.GetMock<IBookRepository>().Verify(x => x.AddBook(It.IsAny<Book>()), Times.Once());
        }
    }
}
