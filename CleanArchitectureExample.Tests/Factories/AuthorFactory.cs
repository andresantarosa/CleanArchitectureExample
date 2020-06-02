using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Tests.Factories
{
    public static class AuthorFactory
    {
        public static Author ReturnAuthor()
        {
            return new Author(Guid.NewGuid(), "Stephen King");
        }
    }
}
