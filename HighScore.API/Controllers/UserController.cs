﻿using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

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

                return StatusCode(400, JObject.FromObject(new { 
                    title = $"User with a name {userData.Name} already exists",
                    status = 400,
                    traceId = HttpContext.TraceIdentifier
                
                }));
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

            if (!string.IsNullOrEmpty(name))
            {
                expression = expression.And((users) => users.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                expression = expression.And((users) => users.Name.Contains(searchQuery));
            }

            var collection = await _userRepository.Find(expression, pageNumber, pageSize);

            if(collection == null)
            {
                return NotFound();
            }

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

        [HttpPatch(("{userId}/highscores/{highScoreId}"))]
        public async Task<ActionResult> PatchUserHighScore(int userId, int highScoreId, JsonPatchDocument<HighScoreWriteDataDTO> patchDocument)
        {
            var highScoreToPatch = (await _highScoreRepository.Find((highscore) => highscore.UserId == userId && highscore.Id == highScoreId)).FirstOrDefault();

            if (highScoreToPatch == null)
            {
                return NotFound();
            }

            HighScoreWriteDataDTO highScorePatched = new HighScoreWriteDataDTO()
            {
                Score = highScoreToPatch.Score,
                UserId = highScoreToPatch.UserId
            };

            patchDocument.ApplyTo(highScorePatched, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //need to check modelstate again due to usage in patch document
            if (!TryValidateModel(highScorePatched))
            {
                return BadRequest(ModelState);
            }

            //update resource
            _mapper.Map(highScorePatched, highScoreToPatch);
            await _highScoreRepository.SaveChanges();

            return NoContent();

        }

        [HttpGet(("{userId}/highscores"), Name = "GetUserHighScores")]
        public async Task<ActionResult<IEnumerable<HighScoreDTO>>> GetUserHighScores(int userId)
        {
            var highScoreQuery = await _highScoreRepository.Find((highscore) => highscore.UserId == userId);

            if (highScoreQuery.Count == 0)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<HighScoreDTO>>(highScoreQuery));
        }

        [HttpPost("{userId}/highscores")]
        public async Task<ActionResult> PostUserHighScore(int userId, HighScoreWriteDataDTO highScore)
        {
            var users = await _userRepository.Find((userData) => userData.Id == userId);

            if (users == null)
            {
                return NotFound();
            }

            highScore.UserId = userId;

            HighScoreEntity highscoreAdded = await _highScoreRepository.Add(_mapper.Map<HighScoreEntity>(highScore));

            var routeValues = new { userId = userId };

            //https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
                //dobrý na tom je, že to nejspíš vynechá tělo procedury a rovnou to jde do returnu
                //protože jsem jednu metodu v těle měl s throwem kvuli chybejici implementaci
                //a i tak to routu zvládlo vytvořit a vrátit
            return CreatedAtRoute("GetUserHighScores", routeValues, _mapper.Map<HighScoreDTO>(highscoreAdded));
        }
    }
}
