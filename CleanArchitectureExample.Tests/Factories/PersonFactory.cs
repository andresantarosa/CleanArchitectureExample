using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Tests.Factories
{
    public static class PersonFactory
    {
        public static Person ReturnPerson()
        {
            return new Person(Guid.NewGuid(), "32145678996", "Andre Santarosa", "user@email.com");
        }
    }
}
