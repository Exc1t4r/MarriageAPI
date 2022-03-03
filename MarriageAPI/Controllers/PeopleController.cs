using MarriageAPI.Models;
using MarriageAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarriageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleService;

        public PeopleController(PeopleService peopleService)
        {
            _peopleService = peopleService;
        }

        // GET: PeopleController/Details/5
        [HttpGet("details/{id}")]
        public ActionResult<Person> Details(int id)
        {
             return _peopleService.GetPerson(id);
        }

        // POST: PeopleController/Add
        [HttpPost("add")]
        public async Task<ActionResult> Add(Person person)
        {
            if (await _peopleService.AddPerson(person))
            {
                return Ok();
            }

            //exception message not implemented
            return BadRequest();
        }

        //POST: PeopleController/Delete/5
        [HttpPost("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            if (await _peopleService.DeletePerson(id))
            {
                return Ok();
            }

            //exception message not implemented
            return BadRequest();
        }
    }
}
