using CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Queries.GetAll
{
    public class GetAllPeopleQueryHandler : IRequestHandler<GetAllPeopleQuery, GetAllPeopleQueryViewModel>
    {
        private readonly IPersonRepository _personRepository;
        public GetAllPeopleQueryHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<GetAllPeopleQueryViewModel> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
        {
            var result = new GetAllPeopleQueryViewModel();

            var allPeople = await _personRepository.GetAll();

            allPeople.ForEach(person =>
            {
                result.Person.Add(new GetAllPeopleQueryViewModel.AddPersonCommandResultViewPerson()
                {
                    PersonId = person.PersonId,
                    Document = person.Document,
                    Name = person.Name
                });
            });

            return result;
        }
    }
}
