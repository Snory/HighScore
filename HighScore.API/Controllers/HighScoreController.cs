
using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HighScore.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HighScoreController : ControllerBase
    {
        private IRepository<HighScoreEntity> _highScoreRepository;
        private IMapper _mapper;
   
        public HighScoreController(IRepository<HighScoreEntity> highScoreRepository, IMapper mapper)
        {
            _highScoreRepository = highScoreRepository ?? throw new ArgumentNullException(nameof(highScoreRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        [HttpGet(("highscores"))]
        public async Task<ActionResult<IEnumerable<HighScoreDTO>>> GetHighScores(int pageNumber = 1, int pageSize = 10)
        {
            var expression = ExpressionBuilder.CreateExpression<HighScoreEntity>((users) => 1 == 1);
            var collection = await _highScoreRepository.Find(expression);

            if (collection.Count == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<List<HighScoreDTO>>(collection));

        }

        [HttpDelete("highscores/{highScoreId}")]
        public async Task<ActionResult> DeleteHighScore(int highScoreId)
        {
            var highScoreToDelete = (await _highScoreRepository.Find((highScore) => highScore.Id == highScoreId)).FirstOrDefault();

            if (highScoreToDelete == null)
            {
                return NotFound();
            }

            await _highScoreRepository.Delete(highScoreToDelete);

            return NoContent();
        }



    }


}
