using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories
{
    public interface IBookLoanRepository
    {
        Task<BookLoan> Add(BookLoan bookLoan);
        Task<BookLoan> GetByLoanId(Guid bookLoanId, bool loadBook);

        BookLoan Update(BookLoan bookLoan);
    }
}
