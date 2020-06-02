using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Persistence.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> AddPerson(Person person);
        Task<Person> GetByDocument(string document, bool loadPhone = false);
        Task<List<Person>> GetAll();
    }
}
