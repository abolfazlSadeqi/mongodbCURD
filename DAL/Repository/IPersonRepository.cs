using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository;
public interface IPersonRepository  {
    List<Person> Get();
    Person Get(string id);
    Person Create(Person person);
    void Update(string id, Person person);
    void Remove(string id);

}
