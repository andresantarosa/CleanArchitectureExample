using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.ReturnBook
{
    public class ReturnBookCommand : IRequest<ReturnBookCommandResponseViewModel>
    {
        public ReturnBookCommand(string loanId)
        {
            LoanId = loanId;
        }

        public string LoanId { get; private set; }
    }
}
