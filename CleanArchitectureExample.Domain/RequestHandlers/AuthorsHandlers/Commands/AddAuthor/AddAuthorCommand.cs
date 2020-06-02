using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.RequestHandlers.AuthorsHandlers.Commands.AddAuthor
{
    public class AddAuthorCommand:IRequest<AddAuthorCommandResponseViewModel>
    {
        public AddAuthorCommand(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
