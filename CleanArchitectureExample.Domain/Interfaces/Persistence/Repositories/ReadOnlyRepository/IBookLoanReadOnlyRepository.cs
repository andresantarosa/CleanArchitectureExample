using CleanArchitectureExample.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories.ReadOnlyRepository
{
    public interface IBookLoanReadOnlyRepository
    {
        public Task<List<BookLoan>> GetAll(bool includeTaker, bool includeBook);
    }
}
