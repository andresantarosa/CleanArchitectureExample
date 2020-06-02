using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Queries
{
    public class BookLoanGetAllQuery: IRequest<BookLoanGetAllQueryResponseViewModel>
    {
    }
}
