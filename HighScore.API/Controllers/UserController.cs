using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text.Json;

namespace HighScore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepository<UserEntity> _userRepository;
        private IRepository<HighScoreEntity> _highScoreRepository;
        private IMapper _mapper;


        public UserController(IRepository<UserEntity> userRepository, IRepository<HighScoreEntity> highScoreRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _highScoreRepository = highScoreRepository ?? throw new ArgumentNullException(nameof(highScoreRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<ActionResult> PostUser(UserWriteData userData)
        {
            var userQuery = (await _userRepository.Find((user) => user.Name == userData.Name)).First();

            if (userQuery != null)
            {

                return BadRequest("User with that name already exists");
            }

            UserEntity userAdded = await _userRepository.Add(_mapper.Map<UserEntity>(userData));

            var routeValues = new { userId = userAdded.Id };

            return CreatedAtRoute("GetUser", routeValues, _mapper.Map<UserDTO>(userAdded));
        }
  
        //default routing attribute based on router attribute defined for class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers(string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
   
            var expression = ExpressionBuilder.CreateExpression<UserEntity>((users) => 1==1);
            var orderPredicate = ExpressionBuilder.CreateExpression<UserEntity>((users) => users.Id);

            if (!string.IsNullOrEmpty(name))
            {
                expression = expression.And((users) => users.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                expression = expression.And((users) => users.Name.Contains(searchQuery));
            }

            var (collection, paginatonMetaData) = await _userRepository.Find(expression, orderPredicate, pageNumber, pageSize);

            if(collection.Count == 0)
            {
                return NotFound();
            }

            //ok result should return result set of requested data, not metadata
            //therefore put metadata to header
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginatonMetaData));

            return Ok(_mapper.Map<List<UserDTO>>(collection));
        }

        [HttpGet("{userId}",Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser(int userId)
        {

            var userQuery = (await _userRepository.Find((user) => user.Id == userId)).First();
            
            if(userQuery == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDTO>(userQuery));
        }


        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUser(int userId, UserWriteData userData)
        {
            var userToUpdate = (await _userRepository.Find((user) => user.Id == userId)).FirstOrDefault();

            if (userToUpdate == null)
            {
                return NotFound();
            }

            _mapper.Map(userData,userToUpdate); //this mapping will cause change to the object and therefore updated,lol, but do i like it?
            await _highScoreRepository.SaveChanges();

            return NoContent();
        }

    }
}
