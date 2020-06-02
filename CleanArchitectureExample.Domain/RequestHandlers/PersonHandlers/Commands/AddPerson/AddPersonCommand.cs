using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Commands.AddPerson
{
    public class AddPersonCommand : IRequest<AddPersonCommandResponseViewModel>
    {
        public AddPersonCommand(string document, string name, string email, List<string> phones)
        {
            Document = document;
            Name = name;
            Phones = phones;
            Email = email;
        }

        public string Document { get; private set; }
        public string Name { get; private set; }
        public List<string> Phones { get; private set; }
        public string Email { get; private set; }
    }
}
