using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Events;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan.Events;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan
{

    public class RequestLoanCommandHandler : IRequestHandler<RequestLoanCommand, RequestLoanCommandResponseViewModel>
    {
        private readonly IPersonRepository _personRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IBookLoanRepository _bookLoanRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public RequestLoanCommandHandler(IPersonRepository personRepository, IBookRepository bookRepository, IBookLoanRepository bookLoanRepository, IEventDispatcher eventDispatcher)
        {
            _personRepository = personRepository;
            _bookRepository = bookRepository;
            _bookLoanRepository = bookLoanRepository;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<RequestLoanCommandResponseViewModel> Handle(RequestLoanCommand request, CancellationToken cancellationToken)
        {
            RequestLoanCommandResponseViewModel response = new RequestLoanCommandResponseViewModel();

            if (!request.ValidateBookGuid())
                return response;

            Person person = await _personRepository.GetByDocument(request.PersonDocument,true);
            if (!request.ValidatePersonNotNul(person))
                return response;

            Book book = await _bookRepository.GetById(Guid.Parse(request.BookId));
            if (!request.ValidateBookNotNul(book))
                return response;

            BookLoan bookLoan = new BookLoan(Guid.NewGuid(), book, person);
            if (!bookLoan.Validate())
                return response;

            if (!bookLoan.LendBook())
                return response;

            await _bookLoanRepository.Add(bookLoan);

            response.LoanId = bookLoan.BookLoanId;
            response.ExpectedReturnDate = bookLoan.ExpectedReturnDate;

            LoanEffetivatedEvent loanEvent = new LoanEffetivatedEvent(person.Name, person.PhoneNumbers?.FirstOrDefault()?.PhoneNumber, book.Title, "", bookLoan.ExpectedReturnDate);
            _eventDispatcher.AddAfterCommitEvent(loanEvent);

            return response;

        }
    }
}
