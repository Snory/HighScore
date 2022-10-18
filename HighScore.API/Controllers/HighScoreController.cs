using HighScore.API.Models;
using HighScore.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HighScore.API.Controllers
{
    [Route("api/users/{userId}/highscores")]
    [ApiController]
    public class HighScoreController : ControllerBase
    {
        private IRepository<HighScoreDTO> _highScoreRepository = HighScoreInMemoryRepository.Instance;
        private IRepository<UserDTO> _userRepository = UserInMemoryRepository.Instance;

        [HttpGet(Name = "GetUserHighScores")]
        public ActionResult<IEnumerable<HighScoreDTO>> GetUserHighScores(int userId)
        {
            var highScoreQuery = _highScoreRepository.Find((highscore) => highscore.UserId == userId).ToList();

            if(highScoreQuery.Count == 0)
            {
                return NotFound();
            }

            return Ok(highScoreQuery);
        }

        [HttpPost]
        public ActionResult<HighScoreDTO> PostUserHighScore(int userId, HighScorePostDTO highScore)
        {
            if(_userRepository.Find((user) => user.Id == userId) == null)
            {
                return NotFound();
            }


            HighScoreDTO createdResource = new HighScoreDTO() { Id = 1, Score = highScore.Score, UserId = userId };

            _highScoreRepository.Add(createdResource);

            var routeValues = new { userId = userId };

            //https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            return CreatedAtRoute("GetUserHighScores", routeValues, createdResource);
        }


    }

   
}
