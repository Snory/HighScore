
using HighScore.Data.Repositories;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HighScore.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HighScoreController : ControllerBase
    {
        private IRepository<HighScoreDTO> _highScoreRepository;
        private IRepository<UserDTO> _userRepository;

        public HighScoreController(IRepository<HighScoreDTO> highScoreRepository, IRepository<UserDTO> userRepository)
        {
            _highScoreRepository = highScoreRepository;
            _userRepository = userRepository;
        }

        [HttpGet(("highscores"))]
        public async Task<ActionResult<IEnumerable<HighScoreDTO>>> GetHighScores()
        {
            var highScoreQuery = (await _highScoreRepository.GetAll()).ToList();

            if (highScoreQuery.Count == 0)
            {
                return NoContent();
            }

            return Ok(highScoreQuery);

        }


        [HttpGet(("users/{userId}/highscores"), Name = "GetUserHighScores")]
        public async Task<ActionResult<IEnumerable<HighScoreDTO>>> GetUserHighScores(int userId)
        {
            var highScoreQuery = ( await _highScoreRepository.Find((highscore) => highscore.UserId == userId)).ToList();

            if(highScoreQuery.Count == 0)
            {
                return NotFound();
            }

            return Ok(highScoreQuery);
        }

        [HttpPost("users/{userId}/highscores")]
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

        [HttpDelete("highscores/{highScoreId}")]
        public async  Task<ActionResult> DeleteHighScore(int highScoreId)
        {

            var highScoreToDelete = (await _highScoreRepository.Find((highScore) => highScore.Id == highScoreId)).FirstOrDefault();

            if(highScoreToDelete == null)
            {
                return NotFound();
            }

            await _highScoreRepository.Delete(highScoreToDelete);

            return NoContent();
        }


        [HttpPatch(("users/{userId}/highscores"))]
        public async Task<ActionResult> PatchUserHighScore(int userId, JsonPatchDocument<HighScoreWriteDataDTO> patchDocument)
        {
            var highScoreQuery = (await _highScoreRepository.Find((highscore) => highscore.UserId == userId)).ToList();

            if (highScoreQuery.Count == 0)
            {
                return NotFound();
            }

            if(highScoreQuery.Count > 1)
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

    }

   
}
