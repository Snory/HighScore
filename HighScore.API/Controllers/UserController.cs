using HighScore.Data.Repositories;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;



namespace HighScore.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IRepository<UserDTO> _userRepository;
        private IRepository<HighScoreDTO> _highScoreRepository;


        public UserController(IRepository<UserDTO> userRepository, IRepository<HighScoreDTO> highScoreRepository)
        {
            _userRepository = userRepository;
            _highScoreRepository = highScoreRepository;
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

        [HttpPatch(("{userId}/highscores"))]
        public async Task<ActionResult> PatchUserHighScore(int userId, JsonPatchDocument<HighScoreWriteDataDTO> patchDocument)
        {
            var highScoreQuery = (await _highScoreRepository.Find((highscore) => highscore.UserId == userId)).ToList();

            if (highScoreQuery.Count == 0)
            {
                return NotFound();
            }

            if (highScoreQuery.Count > 1)
            {
                return BadRequest();
            }

            var patchedResource = highScoreQuery.First();

            HighScoreWriteDataDTO highScoreToPatch = new HighScoreWriteDataDTO()
            {
                Score = patchedResource.Score

            };

            patchDocument.ApplyTo(highScoreToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //need to check modelstate again due to usage in patch document
            if (!TryValidateModel(highScoreToPatch))
            {
                return BadRequest(ModelState);
            }

            //update resource
            patchedResource.Score = highScoreToPatch.Score;

            return NoContent();

        }

        [HttpGet(("{userId}/highscores"), Name = "GetUserHighScores")]
        public async Task<ActionResult<IEnumerable<HighScoreDTO>>> GetUserHighScores(int userId)
        {
            var highScoreQuery = (await _highScoreRepository.Find((highscore) => highscore.UserId == userId)).ToList();

            if (highScoreQuery.Count == 0)
            {
                return NotFound();
            }

            return Ok(highScoreQuery);
        }

        [HttpPost("{userId}/highscores")]
        public async Task<ActionResult> PostUserHighScore(int userId, HighScoreWriteDataDTO highScore)
        {
            var users = await _userRepository.Find((user) => user.Id == userId);

            if (users == null)
            {
                return NotFound();
            }

            HighScoreDTO createdResource = new HighScoreDTO() { Id = 1, Score = highScore.Score, UserId = userId };

            await _highScoreRepository.Add(createdResource);

            var routeValues = new { userId = userId };

            //https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtRoute("GetUserHighScores", routeValues, createdResource);
        }

    }
}
