using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan
{
    public class RequestLoanCommand: IRequest<RequestLoanCommandResponseViewModel>
    {
        public RequestLoanCommand(string personDocument, string bookId)
        {
            PersonDocument = personDocument;
            BookId = bookId;
        }

        public string PersonDocument { get; private set; }
        public string BookId { get; private set; }
    }
}
