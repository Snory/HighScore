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

        [HttpGet]
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


            HighScoreDTO newHighScore = new HighScoreDTO() { Id = 1, Score = highScore.Score, UserId = userId };

            _highScoreRepository.Add(newHighScore);
            

            return NoContent();
        }
    }

   
}
