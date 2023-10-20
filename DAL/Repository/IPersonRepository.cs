using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository;
public interface IPersonRepository
{

    Task<Person> Create(Person Person);

    Task<List<Person>> Get();

    Task<Person> Get(string id);

    Task Remove(string id);

    Task Update(string id, Person Person);

    Task<List<Person>> FindByLastNameFirstName(string FirstName, string LastName);

    Task<List<PersonByLastNameEmail>> GetAllLastName_Email();

}
