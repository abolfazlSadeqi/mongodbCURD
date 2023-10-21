using DAL.Model;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using API.Dto;
using AutoMapper;
namespace API.Controller;

[ApiController]
[Route("[controller]")]
public class ApiPersonController : ControllerBase
{
    private readonly IPersonRepository PersonRepository;

    public ApiPersonController(IPersonRepository PersonRepository)
    {
        this.PersonRepository = PersonRepository;
    }

    [HttpGet("GetAllPersons")]
    public async Task<IEnumerable<Person>> Get() => await PersonRepository.Get();

    [HttpGet("GetAllLastName_Email")]
    public async Task<IEnumerable<PersonByLastNameEmail>> GetAllLastName_Email() => await PersonRepository.GetAllLastName_Email();


    [HttpGet("FindByLastNameFirstName")]
    public async Task<IEnumerable<Person>> FindByLastNameFirstName([FromQuery] PersonSearchDto PersonSearchDto)
           => await PersonRepository.FindByLastNameFirstName(PersonSearchDto.FirstName, PersonSearchDto.LastName);



    [HttpGet("GetByID")]
    public async Task<ActionResult<Person>> Get(string id) => await PersonRepository.Get(id);




    [HttpPost("Insert")]
    public async Task<ActionResult<Person>> Post([FromBody] Person Person)
    {
        await PersonRepository.Create(Person);
        return Ok("Inserted");
    }

    [HttpPost("BulkInsert")]
    public async Task<ActionResult<Person>> BulkInsert([FromBody] List< Person> Persons)
    {
        await PersonRepository.BulkInsert(Persons);
        return Ok("BulkInsert");
    }


    [HttpPut("Update")]
    public async Task<ActionResult> Put(string id, [FromBody] Person Person)
    {
        var existingPerson = PersonRepository.Get(id);

        if (existingPerson == null) return NotFound("not found");
        PersonRepository.Update(id, Person);

        return NoContent();
    }


    [HttpDelete("Delete")]
    public async Task<ActionResult> Delete(string id)
    {
        var Person = PersonRepository.Get(id);

        if (Person == null) return NotFound("not found");
        PersonRepository.Remove(id);

        return Ok("deleted");
    }


}
