using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace projektf.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class BoatController : ControllerBase
    {
        [HttpGet]
        [Route("questions/all")]
        public ActionResult M1()
        {
            Models.HajosContext context = new Models.HajosContext();
            var kérdések = from x in context.Questions select x.Question1;

            return new JsonResult(kérdések);

        }
    }
}
