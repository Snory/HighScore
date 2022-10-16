using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace HighScore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //default routing attribute based on router attribute defined for class
        [HttpGet]
        public JsonResult GetUsers()
        {
            return new JsonResult(
                new List<Object>
                {
                    new { id = 1, Name = "Bruce Willis"},
                    new { id = 2, Name = "Jackie Chan"}
                }
            );
        }
    }
}
