
using DAL.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DAL.Repository;




public class PersonRepository : IPersonRepository
{
    private readonly IMongoCollection<Person> _Persons;
    private const string _CollectionName = "Person";
    public PersonRepository(IMgDatabaseSettings settings, IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase(settings.DatabaseName);
        _Persons = database.GetCollection<Person>(_CollectionName);


    }


    public async Task<Person> Create(Person Person)
    {


        _Persons.InsertOne(Person);


        return Person;





    }

    public async Task<List<Person>> Get() => await _Persons.FindAsync(Person => true).Result.ToListAsync();

    public async Task<List<PersonByLastNameEmail>> GetAllLastName_Email()
    {


        FindOptions<Person, PersonByLastNameEmail> findOptions = new FindOptions<Person, PersonByLastNameEmail>()
        {
            Projection = Builders<Person>.Projection.Include(f => f.LastName).Include(f => f.Email),
            Sort = Builders<Person>.Sort.Ascending(u => u.FirstName).Descending(u => u.LastName)
        };

        return await _Persons.FindAsync(Builders<Person>.Filter.Empty, findOptions).Result.ToListAsync();


    }


    public async Task<List<Person>> FindByLastNameFirstName(string FirstName, string LastName)
    {

        var indexes = await (await _Persons.Indexes.ListAsync()).ToListAsync();


        var IsIndex = indexes
       .SelectMany(i => i.Elements)
       .Any(e => string.Equals(e.Name, "Ix_Email", StringComparison.CurrentCultureIgnoreCase));

        if (!IsIndex)
        {
            var indexKeys = Builders<Person>.IndexKeys.Ascending(a => a.Email);
            var createIndex = new CreateIndexModel<Person>(indexKeys, new CreateIndexOptions()
            {
                Unique = false,
                Name = "Ix_Email"
            });
            await _Persons.Indexes.CreateOneAsync(createIndex);
        }


        var builder = Builders<Person>.Filter;


        var filter = builder.Eq(f => f.FirstName, FirstName) & builder.Eq(f => f.LastName, LastName);


        var query = await _Persons.FindAsync(filter).Result.ToListAsync();






        return query;
    }


    public async Task<Person> Get(string id) => await _Persons.FindAsync(Person => Person.ID == id).Result.FirstOrDefaultAsync();


    public async Task Remove(string id)
    {
        _Persons.DeleteOneAsync(Person => Person.ID == id);
    }

    public async Task Update(string id, Person Person)
    {
        _Persons.ReplaceOneAsync(Person => Person.ID == id, Person);
    }
}