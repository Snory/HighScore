﻿using HighScore.Data.Repositories;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.Mvc;



namespace HighScore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepository<UserDTO> _userRepository;

        public UserController(IRepository<UserDTO> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserWriteData userData)
        {
            UserDTO createdResource = new UserDTO() { Id = 1, Name = userData.Name };
            await _userRepository.Add(createdResource);

            var routeValues = new { userId = createdResource.Id };

            return CreatedAtRoute("GetUser", routeValues, createdResource);
        }
        
        //default routing attribute based on router attribute defined for class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            var usersQuery = await _userRepository.GetAll();

            if (usersQuery == null)
            {
                return NotFound();
            }

            return Ok(usersQuery);

        }

        [HttpGet("{userId}",Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int userId)
        {
            var userQuery = (await _userRepository.Find((user) => user.Id == userId)).First();
            
            if(userQuery == null)
            {
                return NotFound();
            }

            return Ok(userQuery);
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(int userId, UserWriteData user)
        {
            var userToUpdate = (await _userRepository.Find((user) => user.Id == userId)).FirstOrDefault();

            if(userToUpdate == null)
            {
                return NotFound();
            }

            userToUpdate.Name = user.Name;

            return NoContent();
        } 

    }
}
