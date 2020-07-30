using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan;
using CleanArchitectureExample.Domain.Resources;
using CleanArchitectureExample.Tests.Base;
using CleanArchitectureExample.Tests.Factories;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitectureExample.Tests.BookLoanTests.Commands
{
    public class RequestLoanCommandTests: TestBaseArrangements
    {
        public RequestLoanCommandTests():base()
        {
        }

        [Theory]
        [InlineData(" ", "Book Guid string is empty")]
        [InlineData("00000000-0000-0000-0000-000000000000", "Book Guid is empty")]
        async Task RequestLoanCommand_WithInvalidBookGuid_ShouldReturnError(string bookGuid, string outputError)
        {
            //arrage
            RequestLoanCommand command = new RequestLoanCommand("", bookGuid);
            var sut = Mocker.CreateInstance<RequestLoanCommandHandler>();
            //act
            await sut.Handle(command, new CancellationToken());
            //assert
            DomainNotifications.GetAll().Should().NotBeNullOrEmpty()
                                                          .And.HaveCount(1)
                                                          .And.Contain(x => x == outputError);
            Mocker.GetMock<IPersonRepository>().Verify(x => x.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()), Times.Never());
        }

        [Fact]
        async Task RequestLoanCommand_WithInvalidPerson_ShouldReturnPersonNotFound()
        {
            //arrage
            RequestLoanCommand command = new RequestLoanCommand("", Guid.NewGuid().ToString());

            Mocker.GetMock<IPersonRepository>()
                                   .Setup(p => p.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()))
                                   .Returns(() => Task.FromResult<Person>(null));

            var sut = Mocker.CreateInstance<RequestLoanCommandHandler>();

            //act
            await sut.Handle(command, new CancellationToken());

            //Assert
            Mocker.GetMock<IPersonRepository>().Verify(x => x.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
            Mocker.GetMock<IBookLoanRepository>().Verify(x => x.Add(It.IsAny<BookLoan>()), Times.Never);
            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                .And.HaveCount(1)
                                                .And.Contain(x => x == Messages.BookLoan_PersonNotFound);
        }

        [Fact]
        async Task RequestLoanCommand_WithInvalidBook_ShoudlReturnBookNotFound()
        {
            //arrage
            RequestLoanCommand command = new RequestLoanCommand("12345678998", Guid.NewGuid().ToString());

            Mocker.GetMock<IPersonRepository>()
                                  .Setup(p => p.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()))
                                  .Returns(() => Task.FromResult(PersonFactory.ReturnPerson()));

            Mocker.GetMock<IBookRepository>()
                                   .Setup(b => b.GetById(It.IsAny<Guid>()))
                                   .Returns(() => Task.FromResult<Book>(null));
            var sut = Mocker.CreateInstance<RequestLoanCommandHandler>();
            //act
            await sut.Handle(command, new CancellationToken());

            //assert
            Mocker.GetMock<IPersonRepository>().Verify(x => x.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
            Mocker.GetMock<IBookRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
            Mocker.GetMock<IBookLoanRepository>().Verify(x => x.Add(It.IsAny<BookLoan>()), Times.Never);

            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                .And.HaveCount(1)
                                                .And.Contain(x => x == Messages.BookLoan_BookNotFound);
        }

        [Fact]
        async Task RequestLoanCommand_WithValidInfos_ShoudlReturnSuccess()
        {
            //arrage
            RequestLoanCommand command = new RequestLoanCommand("12345678998", Guid.NewGuid().ToString());

            Mocker.GetMock<IPersonRepository>()
                                  .Setup(p => p.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()))
                                  .Returns(() => Task.FromResult(PersonFactory.ReturnPerson()));

            Mocker.GetMock<IBookRepository>()
                                   .Setup(b => b.GetById(It.IsAny<Guid>()))
                                   .Returns(() => Task.FromResult<Book>(BookFactory.ReturnBook()));
            var sut = Mocker.CreateInstance<RequestLoanCommandHandler>();
            //act
            await sut.Handle(command, new CancellationToken());

            //assert
            Mocker.GetMock<IPersonRepository>().Verify(x => x.GetByDocument(It.IsAny<string>(), It.IsAny<bool>()), Times.Once());
            Mocker.GetMock<IBookRepository>().Verify(x => x.GetById(It.IsAny<Guid>()), Times.Once());
            Mocker.GetMock<IBookLoanRepository>().Verify(x => x.Add(It.IsAny<BookLoan>()), Times.Once);

            DomainNotifications.GetAll().Should().BeEmpty();
        }
    }
}
