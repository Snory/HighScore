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
        public ActionResult<IEnumerable<UserDTO>> GetUsers()
        {
            var usersQuery = _userRepository.GetAll();

            if (usersQuery == null)
            {
                return NotFound();
            }

            return Ok(usersQuery);

        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            var userQuery = _userRepository.Find((user) => user.Id == id).First();
            
            if(userQuery == null)
            {
                return NotFound();
            }

            return Ok(userQuery);
        }

        [HttpPut("{userId}")]
        public ActionResult UpdateUser(int userId, UserUpdateDTO user)
        {
            var userToUpdate = _userRepository.Find((user) => user.Id == userId).FirstOrDefault();

            if(userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Name = user.Name;

            return NoContent();
        } 

    }
}
