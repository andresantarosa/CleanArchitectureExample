using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.ReturnBook
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, ReturnBookCommandResponseViewModel>
    {
        private readonly IBookLoanRepository _bookLoanRepository;

        public ReturnBookCommandHandler(IBookLoanRepository bookLoanRepository)
        {
            _bookLoanRepository = bookLoanRepository;
        }

        public async Task<ReturnBookCommandResponseViewModel> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            ReturnBookCommandResponseViewModel response = new ReturnBookCommandResponseViewModel();

            if (!request.ValidateBookLoanGuid())
                return response;

            BookLoan bookLoan = await _bookLoanRepository.GetByLoanId(Guid.Parse(request.LoanId), true);
            
            if (!request.ValidateBookLoanExists(bookLoan))
                return response;

            if (!bookLoan.ReturnBook())
                return response;

            _bookLoanRepository.Update(bookLoan);

            return response;
        }
    }
}
