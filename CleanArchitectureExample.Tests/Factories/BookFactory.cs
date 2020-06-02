using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Tests.Factories
{
    public static class BookFactory
    {
        public static Book ReturnBook()
        {
            return new Book(Guid.NewGuid(), "New book", 2020, 1, "1231231231233", AuthorFactory.ReturnAuthor());
        }
    }
}
