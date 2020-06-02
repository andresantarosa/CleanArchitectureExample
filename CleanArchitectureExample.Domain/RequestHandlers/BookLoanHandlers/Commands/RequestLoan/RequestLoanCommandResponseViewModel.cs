using CleanArchitectureExample.Domain.Interfaces.Requests;
using System;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan
{
    public class RequestLoanCommandResponseViewModel: IRequestResponse
    {
        public Guid LoanId { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}
