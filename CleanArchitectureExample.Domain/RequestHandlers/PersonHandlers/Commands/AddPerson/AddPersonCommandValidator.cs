using CleanArchitectureExample.Domain.Core.Validators;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Resources;

namespace CleanArchitectureExample.Domain.RequestHandlers.PersonHandlers.Commands.AddPerson
{
    public static class AddPersonCommandValidator
    {

        public static bool HasPersonNull(this AddPersonCommand command, Person person)
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsNull(person, Messages.Person_PersonWithDocumentExists));
        }
    }
}
