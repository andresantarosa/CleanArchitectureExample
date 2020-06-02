using MediatR;
using System;

namespace CleanArchitectureExample.Domain.RequestHandlers.BookLoanHandlers.Commands.RequestLoan.Events
{
    public class LoanEffetivatedEvent: INotification
    {
        public LoanEffetivatedEvent(string name, string phone, string bookName, string email, DateTime returnDate)
        {
            Name = name;
            Phone = phone;
            BookName = bookName;
            Email = email;
            ReturnDate = returnDate;
        }

        public string Name { get; private set; }
        public string Phone { get; private set; }
        public string BookName { get; private set; }
        public string Email { get; private set; }
        public DateTime ReturnDate { get; private set; }
    }
}
