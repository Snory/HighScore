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
        private HighScoreInMemoryRepository _highScoreRepository = HighScoreInMemoryRepository.Instance;

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

    }
}
