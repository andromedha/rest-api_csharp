using System.Collections.Generic;
using DataClasses;
using Microsoft.AspNetCore.Mvc;
using PersonManagement.Contracts;

namespace Uebung_RestAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonManager _personManager;

        public PersonsController(IPersonManager personManager)
        {
            _personManager = personManager;
        }

        [HttpGet("{id}")]
        public Person Get (int id)
        {  
            return _personManager.GetPerson(id);
        }


        [HttpGet]
        public IEnumerable<Person> Get()
        {
            return _personManager.GetPersons();
        }

        [HttpGet("color/{color}")]
        public IEnumerable<Person> Color (string color)
        {
            return _personManager.GetPersonsWithColor(color);
        }

        [HttpPost]
        public Person Post (Person person)
        {
            return _personManager.AddPerson(person);
        }
    }
}