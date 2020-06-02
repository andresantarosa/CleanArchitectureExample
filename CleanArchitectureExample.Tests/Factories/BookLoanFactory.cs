using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Tests.Factories
{
    public static class BookLoanFactory
    {
        public static BookLoan ReturnLoan()
        {
            return new BookLoan(Guid.NewGuid(), BookFactory.ReturnBook(), PersonFactory.ReturnPerson());
        }
    }
}
