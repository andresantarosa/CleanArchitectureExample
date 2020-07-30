using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using CleanArchitectureExample.Domain.RequestHandlers.AuthorsHandlers.Commands.AddAuthor;
using CleanArchitectureExample.Tests.Base;
using FluentAssertions;
using Moq;
using System.Threading;
using Xunit;

namespace CleanArchitectureExample.Tests.AuthorTests.Commands
{
    public class AddAuthorCommandTest : TestBaseArrangements
    {
        public AddAuthorCommandTest() : base()
        {
        }

        [Theory]
        [InlineData("")]
        [InlineData("Abc")]
        [InlineData(null)]
        public async void HandleAddAuthorCommand_WithInvalidMinimumChars_ShouldReturnError(string authorName)
        {
            //Arrange
            AddAuthorCommand request = new AddAuthorCommand(authorName);
            var sut = Mocker.CreateInstance<AddAuthorCommandHandler>();
            //Act
            await sut.Handle(request, new CancellationToken());

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty();
            Mocker.GetMock<IAuthorRepository>().Verify(x => x.AddAuthor(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async void HandleAddAuthorCommand_WithValidAuthor_ShouldReturnNoErrors()
        {
            //Arrange
            AddAuthorCommand request = new AddAuthorCommand("Stephen King");
            var sut = Mocker.CreateInstance<AddAuthorCommandHandler>();

            //Act
            await sut.Handle(request, new CancellationToken());

            //Assert
            DomainNotifications.GetAll().Should().BeEmpty();
            Mocker.GetMock<IAuthorRepository>().Verify(x => x.AddAuthor(It.IsAny<Author>()), Times.Once);
        }
    }
}
