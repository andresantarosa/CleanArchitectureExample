using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories.ReadOnlyRepository;
using CleanArchitectureExample.Persistence.Context;
using CleanArchitectureExample.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Persistence.Repositories.ReadOnlyRepository
{
    public class BookLoanReadOnlyRepository : BaseRepositoryReadOnly<BookLoan>, IBookLoanReadOnlyRepository
    {
        public BookLoanReadOnlyRepository(ApplicationDbContextReadOnly context) : base(context)
        {
        }

        public async Task<List<BookLoan>> GetAll(bool includeTaker, bool includeBook)
        {
            var query = DbSet.AsQueryable();

            if (includeTaker)
                query = query.Include(q => q.Taker);

            if (includeBook)
                query = query.Include(q => q.Book);

            return await query.OrderBy(q => q.CheckoutDate).ToListAsync();
        }

    }
}
