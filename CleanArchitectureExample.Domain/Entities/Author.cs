using CleanArchitectureExample.Domain.Base;
using CleanArchitectureExample.Domain.Core.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitectureExample.Domain.Entities
{
    public class Author : BaseEntity
    {
        public Author(Guid authorId, string name)
        {
            AuthorId = authorId;
            Name = name;
        }

        public Guid AuthorId { get; private set; }
        public string Name { get; private set; }

        #region .:: Navigation Properties ::.
        public virtual ICollection<Book> Books { get; private set; }
        #endregion


        public bool Validate()
        {
            return AssertionsConcern.IsSatisfiedBy(AssertionsConcern.IsGuidNotNull(AuthorId, Resources.Messages.Author_AuthorGuidIsNull),
                                                   AssertionsConcern.IsGuidNotEmpty(AuthorId, Resources.Messages.Author_AuthorGuidIsEmpty),
                                                   AssertionsConcern.IsNotNull(Name, Resources.Messages.Author_AuthorNameIsNull),
                                                   AssertionsConcern.IsGreaterThanOrEquals(Name?.Length, 5, Resources.Messages.Author_AuthorNameShouldHaveAtLeastFiveChars),
                                                   AssertionsConcern.IsLowerOrEquals(Name?.Length, 100, Resources.Messages.Author_AuthorNameNoLongerThantAHundredChars));
        }
    }
}
