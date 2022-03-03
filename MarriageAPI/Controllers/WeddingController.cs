using MarriageAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MarriageAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeddingController : Controller
    {
        private readonly WeddingService _weddingService;

        public WeddingController(WeddingService weddingService)
        {
            _weddingService = weddingService;
        }

        // POST: MarriageController/Marry
        [HttpPost("marry")]
        public async Task<ActionResult> Marry(int person1Id, int person2Id)
        {
            if (await _weddingService.GetMarried(person1Id, person2Id))
            {
                return Ok();
            }

            // exception message not implemented
            return BadRequest();
        }

        //POST: MarriageController/Divorce/5
        [HttpPost("divorce")]
        public async Task<ActionResult> Divorce(int id)
        {
            if (await _weddingService.GetDivorced(id))
            {
                return Ok();
            }

            //exception message not implemented
            return BadRequest();
        }
    }
}