
Create a simple  CRUD application with mongodb :

Tech Specification:
----
.mongodb 

.Net7

.Swagger UI

.TDD(XUnit)

.WebAPI


 ## Steps
 
### 1.Install MongoDB.Driver ON nugget
### 2. Create DB 
### 3.Create Collection 
```
var database = mongoClient.GetDatabase(settings.DatabaseName); 
database.CreateCollection(_CollectionName);
```
example
```
private const string _CollectionName = "Person";
public PersonRepository(IMgDatabaseSettings settings, IMongoClient mongoClient)
{

    var database = mongoClient.GetDatabase(settings.DatabaseName); 
    var collectionExists = database.ListCollectionNames().ToList().Contains(_CollectionName);
    if (!collectionExists)  database.CreateCollection(_CollectionName); 

}
```

### 4. Define Document Person
```
[BsonIgnoreExtraElements]
public class Person
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { set; get; }

    [BsonElement("FirstName")]
    public string FirstName { set; get; }

    [BsonElement("LastName")]
    public string LastName { set; get; }

    [BsonElement("Suffix")]
    public string Suffix { set; get; }

    [BsonElement("Email")]
    public string Email { set; get; }

    [BsonElement("AdditionalContactInfo")]
    public string AdditionalContactInfo { set; get; }

    [BsonElement("ModifiedDate")]
    public DateTime ModifiedDate { set; get; }


    [BsonElement("CreateDate")]
    public DateTime CreateDate { set; get; }


}
```
### 5. Get Collection
```
 var database = mongoClient.GetDatabase(settings.DatabaseName); 
 database.GetCollection<objectType>(_CollectionName);
```
example
```
private readonly IMongoCollection<Person> _Persons;
 private const string _CollectionName = "Person";
 public PersonRepository(IMgDatabaseSettings settings, IMongoClient mongoClient)
 {

     var database = mongoClient.GetDatabase(settings.DatabaseName); 

     _Persons = database.GetCollection<Person>(_CollectionName); 

 }
```
### 6. add config to appsetting 
```
"MgDatabaseSettings": {
  "ConnectionString": "mongodb://IP:Port",
  "DatabaseName": "DataBaseName"
},
```
example
```
"MgDatabaseSettings": {
  "ConnectionString": "mongodb://localhost:27017",
  "DatabaseName": "TestDB"
},
```
### 7. Define MgDatabaseSettings
```
public class MgDatabaseSettings : IMgDatabaseSettings
{

    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }

}

public interface IMgDatabaseSettings
{

    string ConnectionString { get; set; }
    string DatabaseName { get; set; }

}
```
### 8.add configuration in Program or startup 
```
services.Configure<MgDatabaseSettings>(
                Configuration.GetSection(nameof(MgDatabaseSettings)));

services.AddSingleton<IMgDatabaseSettings>(sp =>
    sp.GetRequiredService<IOptions<MgDatabaseSettings>>().Value);

services.AddSingleton<IMongoClient>(s =>
        new MongoClient(Configuration.GetValue<string>("MgDatabaseSettings:ConnectionString")));
```
### 9.Using methods (insert, update, …)

#### Insert
```
  _collection.InsertOne(Person);
```
example
```
_Persons.InsertOne(Person);
```
##### Update
```
_collection.ReplaceOneAsync(where clause,new object);
```
example
```
  _Persons.ReplaceOneAsync(Person => Person.ID == id, Person);
```
#### Remove
```
_collection.DeleteOneAsync(where clause);
```
example
```
_Persons.DeleteOneAsync(Person => Person.ID == id);
```

#### Sort
```
FindOptions<objectType> findOptions = new FindOptions< objectType>
{
    Sort = Builders< objectType>.Sort.<TypeSort(Ascending, Descending)>Ascending(sortname)
};

_collection.FindAsync(where clause, findOptions);
```
example
```
FindOptions<Person > findOptions = new FindOptions<Person
{
    Sort = Builders<Person>.Sort.Ascending(u => u.FirstName).Descending(u => u.LastName)
};

_Persons.FindAsync(Builders<Person>.Filter.Empty, findOptions);

```
#### Select
```
FindOptions<objectType> findOptions = new FindOptions< objectType>
{
    Projection = Builders< objectType>.Projection.Include(ColumnName)
 };

_collection.FindAsync(where clause, findOptions);
```
example
```
FindOptions<Person, PersonByLastNameEmail> findOptions = new FindOptions<Person, PersonByLastNameEmail>()
{
    Projection = Builders<Person>.Projection.Include(f => f.LastName).Include(f => f.Email),
};

_Persons.FindAsync(Builders<Person>.Filter.Empty, findOptions);
```

#### Index
 ```
   _collection.Indexes.CreateOneAsync(CreateIndexModel);
```
example
```
var indexes = await (await _Persons.Indexes.ListAsync()).ToListAsync();

 var IsIndex = indexes.SelectMany(i => i.Elements)
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
```


#### Use filter in find
 ```
var builder = Builders<object>.Filter;
 var filter = builder.<where clause>;

 _collection.FindAsync(filter)
```
example
```
 var builder = Builders<Person>.Filter;
 var filter = builder.Eq(f => f.FirstName, FirstName) & builder.Eq(f => f.LastName, LastName);
 _Persons.FindAsync(filter);
```

#### Bulk insert
```
_collection.InsertMany(list object)
```
example
```
_Persons.InsertMany(Persons);
```



