
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
   
        public HighScoreController(IRepository<HighScoreDTO> highScoreRepository)
        {
            _highScoreRepository = highScoreRepository;
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

        

    }

   
}
