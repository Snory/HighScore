using HighScore.API.Models;
using HighScore.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



namespace HighScore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepository<UserDTO> _userRepository = UserInMemoryRepository.Instance;
        
        //default routing attribute based on router attribute defined for class
        [HttpGet]
        public JsonResult GetUsers()
        {
            return new JsonResult(_userRepository.GetAll());
        }
    }
}
