using MarriageAPI.Data;
using MarriageAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MarriageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MarriageController : Controller
    {
        private readonly ApplicationDbContext _appDbContext;

        public MarriageController(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // POST: MarriageController/Marry
        [HttpPost("marry")]
        public async Task<ActionResult<Marriage>> Marry(int person1Id, int person2Id)
        {
            // prevents same person to marry himself
            if (person1Id != person2Id)
            {
                // get's unmarried persons by id
                Person p1 = _appDbContext.People.Where(p => p.Id == person1Id)
                                                .Where(p => p.Married == false)
                                                .SingleOrDefault();

                Person p2 = _appDbContext.People.Where(p => p.Id == person2Id)
                                                .Where(p => p.Married == false)
                                                .SingleOrDefault();

                if (p1 != null && p2 != null)
                {
                    // congratulations!
                    p1.Married = true;
                    p2.Married = true;

                    Marriage marriage = new Marriage
                    {            
                        Person1 = p1,
                        Person2 = p2,
                        Date = DateTime.Now
                    };
            
                    try
                    {
                        _appDbContext.Marriages.Add(marriage);
                        await _appDbContext.SaveChangesAsync();

                        return Ok();
                    }
                    catch
                    {   // exception message not implemented
                        return BadRequest();
                    }
                }           
            }

            // exception message not implemented
            return BadRequest();
        }

        //POST: MarriageController/Divorce/5
        [HttpPost("divorce")]
        public async Task<ActionResult> Divorce(int id)
        {
            try
            {
                Marriage marriages = _appDbContext.Marriages.Include(m => m.Person1).AsSplitQuery()
                                                            .Include(m => m.Person2).AsSplitQuery()
                                                            .Where(m=>m.Id == id)
                                                            .FirstOrDefault();

                if (marriages != null)
                {
                    // congratulations!
                    marriages.Person1.Married = false;
                    marriages.Person2.Married = false;

                    _appDbContext.People.Update(marriages.Person1);
                    _appDbContext.People.Update(marriages.Person2);

                    _appDbContext.Marriages.Remove(marriages);

                    await _appDbContext.SaveChangesAsync();

                    return Ok();
                }

                //exception message not implemented
                return BadRequest();
            }
            catch
            {
                //exception message not implemented
                return BadRequest();
            }
        }
    }
}
