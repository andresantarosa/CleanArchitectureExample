using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Tests.Base;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanArchitectureExample.Tests.AuthorTests.Entities
{
    public class AuthorTests : TestBaseArrangements
    {
        public AuthorTests() : base()
        {
        }

        [Fact]
        public void ValidateAuthor_WithEmptyGuidAndInvalidName_ShouldReturnEmptyGuidAndNameLenghtBellow()
        {
            //Arrange


            Author author = new Author(Guid.Empty, "fdfd");

            //Act
            author.Validate();

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                 .And.Contain(x => x == Domain.Resources.Messages.Author_AuthorGuidIsEmpty)
                                                 .And.Contain(x => x == Domain.Resources.Messages.Author_AuthorNameShouldHaveAtLeastFiveChars)
                                                 .And.HaveCount(2);

        }

        [Fact]
        public void ValidateAuthor_WithInvalidName_ShouldReturnNameLenghtMaxAndNullNameError()
        {

            //Arrange


            Author author = new Author(Guid.NewGuid(), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et ");

            //Act
            author.Validate();

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                 .And.Contain(x => x == Domain.Resources.Messages.Author_AuthorNameNoLongerThantAHundredChars)
                                                 .And.HaveCount(1);

        }

        [Fact]
        public void ValidateAuthor_WithNullName_ShouldReturnNullNameError()
        {
            //Arrange
            Author author = new Author(Guid.NewGuid(), null);

            //Act
            author.Validate();

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty().
                                                         And.Contain(x => x == Domain.Resources.Messages.Author_AuthorNameIsNull);
        }
    }
}
