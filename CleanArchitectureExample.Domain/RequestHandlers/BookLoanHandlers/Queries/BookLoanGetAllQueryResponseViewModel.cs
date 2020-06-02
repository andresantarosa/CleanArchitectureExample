using CleanArchitectureExample.Domain.Interfaces.Requests;
using System;
using System.Collections.Generic;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Queries
{
    public class BookLoanGetAllQueryResponseViewModel: IRequestResponse
    {
        public List<BookLoanGetAllQueryResponseViewModelItem> BookLoans { get; set; } = new List<BookLoanGetAllQueryResponseViewModelItem>();
    }

    public class BookLoanGetAllQueryResponseViewModelItem
    {
        public Guid BookLoanId { get; set; }
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public Guid TakerId { get; set; }
        public string TakerName { get; set; }
        public DateTime CheckoutDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
    }
}