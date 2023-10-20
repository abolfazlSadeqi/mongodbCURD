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

    [HttpGet("GetAll")]
    public ActionResult<List<Person>> Get() => PersonRepository.Get();


   
    [HttpGet("GetByID")]
    public ActionResult<Person> Get(string id) => PersonRepository.Get(id);
    

  
    [HttpPost("Insert")]
    public ActionResult<Person> Post([FromBody] Person Person)
    {
        PersonRepository.Create(Person);
        return CreatedAtAction(nameof(Get), new { id = Person.ID }, Person);
    }

 
    [HttpPut("Update")]
    public ActionResult Put(string id, [FromBody] Person Person)
    {
        var existingPerson = PersonRepository.Get(id);

        if (existingPerson == null)  return NotFound("not found");
        PersonRepository.Update(id, Person);

        return NoContent();
    }

    
    [HttpDelete("Delete")]
    public ActionResult Delete(string id)
    {
        var Person = PersonRepository.Get(id);

        if (Person == null) return NotFound("not found");
        PersonRepository.Remove(Person.ID);

        return Ok("deleted");
    }


}
