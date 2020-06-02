using CleanArchitectureExample.Domain.Interfaces.Requests;
using System;
using System.Collections.Generic;

namespace CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Commands.AddPerson
{
    public class AddPersonCommandResponseViewModel : IRequestResponse
    {
        public Guid PersonId { get; set; }
        public string Document { get; set; }
        public string Name { get; set; }
        public List<AddPersonCommandResultViewModelPhones> PhoneNumbers { get; set; } = new List<AddPersonCommandResultViewModelPhones>();


        public class AddPersonCommandResultViewModelPhones
        {
            public Guid PhoneId { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
