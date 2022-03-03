using MarriageAPI.Data;
using MarriageAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarriageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;

        public PeopleController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: PeopleController/Details/5
        [HttpGet("details/{id}")]
        public ActionResult<Person> Details(int id)
        {
            try
            {
                return _appDbContext.People.Find(id);
            }
            catch
            {
                //exception message not implemented
                return BadRequest();
            }
        }

        // POST: PeopleController/Add
        [HttpPost("add")]
        public async Task<ActionResult<Person>> Add(Person person)
        {
            try
            {
                _appDbContext.People.Add(person);
                await _appDbContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {   // exception message not implemented
                return BadRequest();
            }
        }

        //POST: PeopleController/Delete/5
        [HttpPost("delete")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                Person person = _appDbContext.People.Find(id);

                _appDbContext.People.Remove(person);
                await _appDbContext.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                //exception message not implemented
                return BadRequest();
            }
        }
    }
}
