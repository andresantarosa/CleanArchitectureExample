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
    public class AddAuthorCommandTest
    {
        public AddAuthorCommandTest()
        {
            DomainNotificationsFacade.SetTestingEnvironment();
        }

        [Theory]
        [InlineData("")]
        [InlineData("Abc")]
        [InlineData(null)]
        public async void HandleAddAuthorCommand_WithInvalidMinimumChars_ShouldReturnError(string authorName)
        {
            //Arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            AddAuthorCommand request = new AddAuthorCommand(authorName);
            var sut = baseArrangements.Mocker.CreateInstance<AddAuthorCommandHandler>();
            //Act
            await sut.Handle(request, new CancellationToken());

            //Assert
            baseArrangements.DomainNotifications.GetAll().Should().NotBeEmpty();
            baseArrangements.Mocker.GetMock<IAuthorRepository>().Verify(x => x.AddAuthor(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async void HandleAddAuthorCommand_WithValidAuthor_ShouldReturnNoErrors()
        {
            //Arrange
            TestBaseArrangements baseArrangements = new TestBaseArrangements();
            AddAuthorCommand request = new AddAuthorCommand("Stephen King");
            var sut = baseArrangements.Mocker.CreateInstance<AddAuthorCommandHandler>();
            
            //Act
            await sut.Handle(request, new CancellationToken());

            //Assert
            baseArrangements.DomainNotifications.GetAll().Should().BeEmpty();
            baseArrangements.Mocker.GetMock<IAuthorRepository>().Verify(x => x.AddAuthor(It.IsAny<Author>()), Times.Once);
        }
    }
}
