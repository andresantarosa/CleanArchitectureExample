using CleanArchitectureExample.Domain.Base;
using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Person : BaseEntity
    {
        public Person()
        {

        }

        public Person(Guid personId,
                      string document,
                      string name,
                      string email)
        {
            PersonId = personId;
            Document = document;
            Name = name;
            Email = email;
        }


        public Guid PersonId { get; private set; }
        public string Document { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        #region .:: Navigation Properties ::.
        public virtual ICollection<PersonPhone> PhoneNumbers { get; set; }
        public virtual ICollection<BookLoan> BookLoans { get; set; }

        #endregion

        public Person WithPhoneNumbes(List<PersonPhone> personPhones)
        {
            if (personPhones == null)
                PhoneNumbers = new List<PersonPhone>();

            PhoneNumbers = personPhones;
            return this;
        }

        // Responsible for validate business rules
        public bool Validate()
        {

            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.HasMinimumLength(string.IsNullOrWhiteSpace(Name) ? "" : Name, 5, Resources.Messages.Person_NameMiniumCharError),
                                                   AssertionsConcern.IsStringNotNullOrWhiteSpace(Document, Resources.Messages.Person_DocumentIsEmptyError),
                                                   AssertionsConcern.HasLengthEquals(Document, 11, Resources.Messages.Person_DocumentWrognCharCount),
                                                   AssertionsConcern.IsGreaterThanOrEquals(PhoneNumbers?.Count(), 1, Resources.Messages.Person_ShouldHaveAtLeastOnePhoneNumber));
        }
    }
}
