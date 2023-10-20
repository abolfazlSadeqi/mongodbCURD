
using DAL.Model;
using MongoDB.Driver;

namespace DAL.Repository;




public class PersonRepository : IPersonRepository
{
    private readonly IMongoCollection<Person> _Persons;
    private const string _CollectionName= "Person";

    public PersonRepository(IMgDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _Persons = database.GetCollection<Person>(_CollectionName);
    }

    public Person Create(Person Person)
    {
        _Persons.InsertOne(Person);
        return Person;
    }

    public List<Person> Get()
    {
        return _Persons.Find(Person => true).ToList();
    }

    public Person Get(string id)
    {
        return _Persons.Find(Person => Person.ID == id).FirstOrDefault();
    }

    public void Remove(string id)
    {
        _Persons.DeleteOne(Person => Person.ID == id);
    }

    public void Update(string id, Person Person)
    {
        _Persons.ReplaceOne(Person => Person.ID == id, Person);
    }
}