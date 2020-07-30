using CleanArchitectureExample.Domain.Core.DomainNotification;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Tests.Base;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CleanArchitectureExample.Tests.PersonTests.Entity
{
    public class PersonTests: TestBaseArrangements
    {
        public PersonTests():base()
        {
        }

        [Theory]
        [InlineData("39758357883", "Anne", "user@email.com", "")]
        [InlineData("39758357883", "", "user@email.com", "")]
        [InlineData("39758357883", null, null, null)]
        public async void ValidatePerson_WithInvalidName_ShouldReturnInvalidNameError(string document, string name, string email, string phoneNumber)
        {

            //Arrange

            Person person = null;
            person = new Person(Guid.NewGuid(), document, name, email);
            List<PersonPhone> phones = new List<PersonPhone>();
            phones.Add(new PersonPhone(Guid.NewGuid(), phoneNumber, person));
            person.WithPhoneNumbes(phones);

            //Act
            person.Validate();

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                 .And.Contain(x => x == Domain.Resources.Messages.Person_NameMiniumCharError)
                                                 .And.HaveCount(1);

        }

        [Theory]
        [InlineData("", "Andre", "user@email.com", "")]
        [InlineData("     ", "Andre", "user@email.com", "")]
        [InlineData(null, "Andre", "user@email.com", null)]
        public async void ValidatePerson_WithEmptyDocument_ShouldReturnEmptyDocumentAndCharCountError(string document, string name, string email, string phoneNumber)
        {

            //Arrange

            Person person = null;
            person = new Person(Guid.NewGuid(), document, name, email);
            List<PersonPhone> phones = new List<PersonPhone>();
            phones.Add(new PersonPhone(Guid.NewGuid(), phoneNumber, person));
            person.WithPhoneNumbes(phones);

            //Act
            person.Validate();

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                 .And.Contain(x => x == Domain.Resources.Messages.Person_DocumentIsEmptyError)
                                                 .And.Contain(x => x == Domain.Resources.Messages.Person_DocumentWrognCharCount)
                                                 .And.HaveCount(2);
        }

        [Theory]
        [InlineData("1234567891", "Andrew", "user@email.com", "")]
        [InlineData("1234567897987987", "Andrew", "user@email.com", null)]
        public async void ValidatePerson_WithWrongDocumentCharCount_ShouldReturnDocumentCharCountError(string document, string name, string email, string phoneNumber)
        {

            //Arrange

            Person person = null;
            person = new Person(Guid.NewGuid(), document, name, email);
            List<PersonPhone> phones = new List<PersonPhone>();
            phones.Add(new PersonPhone(Guid.NewGuid(), phoneNumber, person));
            person.WithPhoneNumbes(phones);

            //Act
            person.Validate();

            //Assert
            DomainNotifications.GetAll().Should().NotBeEmpty()
                                                 .And.Contain(x => x == Domain.Resources.Messages.Person_DocumentWrognCharCount)
                                                 .And.HaveCount(1);
        }
    }
}
