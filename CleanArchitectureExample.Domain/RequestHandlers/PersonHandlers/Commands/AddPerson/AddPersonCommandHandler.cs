using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Commands.AddPerson
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, AddPersonCommandResponseViewModel>
    {
        private readonly IPersonRepository _personRepository;

        public AddPersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<AddPersonCommandResponseViewModel> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {
            AddPersonCommandResponseViewModel response = new AddPersonCommandResponseViewModel();

            // Build entity
            Person person = new Person(Guid.NewGuid(), request.Document, request.Name, request.Email);
            var phoneList = request.Phones.Select(x => new PersonPhone(Guid.NewGuid(), x, person)).ToList();
            person.WithPhoneNumbes(phoneList);

            // Domain Validations
            if (!person.Validate())
                return response;

            // Repository validations
            var personInRep = await _personRepository.GetByDocument(request.Document);
            if (!request.HasPersonNull(personInRep))
                return response;

            await _personRepository.AddPerson(person);

            //Build Response
            response.PersonId = person.PersonId;
            response.Document = person.Document;
            response.Name = person.Name;
            person.PhoneNumbers.ToList().ForEach(x =>
            {
                response.PhoneNumbers.Add(new AddPersonCommandResponseViewModel.AddPersonCommandResultViewModelPhones()
                {
                    PhoneId = x.PersonPhoneId,
                    PhoneNumber = x.PhoneNumber
                });
            });
            
            return response;
        }
    }
}
