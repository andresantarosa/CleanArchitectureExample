using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Enums;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories.ReadOnlyRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Queries
{
    public class BookLoanGetAllQueryHandler : IRequestHandler<BookLoanGetAllQuery, BookLoanGetAllQueryResponseViewModel>
    {
        private readonly IBookLoanReadOnlyRepository _bookLoanReadOnlyRepository;

        public BookLoanGetAllQueryHandler(IBookLoanReadOnlyRepository bookLoanReadOnlyRepository)
        {
            _bookLoanReadOnlyRepository = bookLoanReadOnlyRepository;
        }

        public async Task<BookLoanGetAllQueryResponseViewModel> Handle(BookLoanGetAllQuery request, CancellationToken cancellationToken)
        {
            BookLoanGetAllQueryResponseViewModel response = new BookLoanGetAllQueryResponseViewModel();

            List<BookLoan> bookLoans = await _bookLoanReadOnlyRepository.GetAll(true, true);

            bookLoans.ForEach(x =>
            {
                response.BookLoans.Add(new BookLoanGetAllQueryResponseViewModelItem
                {
                    BookLoanId = x.BookLoanId,
                    BookId = x.BookId,
                    BookTitle = x.Book.Title,
                    TakerId = x.Taker.PersonId,
                    TakerName = x.Taker.Name,
                    CheckoutDate = x.CheckoutDate,
                    ExpectedReturnDate = x.ExpectedReturnDate
                });
            });

            return response;

        }
    }
}