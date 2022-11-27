
using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace HighScore.API.Controllers
{
    [Route("api/highscores")]
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
 

        [HttpGet("{leaderBoardId}")]
        public async Task<ActionResult<IEnumerable<HighScoreDTO>>> GetLeaderBoardHighScores(int leaderBoardId, int pageNumber = 1, int pageSize = 10)
        {
            var expression = ExpressionBuilder.CreateExpression<HighScoreEntity>((highScores) => highScores.LeaderBoardId == leaderBoardId);
            var collection = await _highScoreRepository.Find(expression);

            if (collection.Count == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<List<HighScoreDTO>>(collection));

        }

        [HttpDelete("{highScoreId}")]
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


        [HttpPost]
        public async Task<ActionResult> PostHighScore(HighScoreWriteDataDTO highScoreData)
        {

            HighScoreEntity highScoreAdded =
                await _highScoreRepository.Add(_mapper.Map<HighScoreEntity>(highScoreData));

            var routeValues = new { leaderBoardId = highScoreAdded.LeaderBoardId };


            //https://ochzhen.com/blog/created-createdataction-createdatroute-methods-explained-aspnet-core
            //dobrý na tom je, že to nejspíš vynechá tělo procedury a rovnou to jde do returnu
            //protože jsem jednu metodu v těle měl s throwem kvuli chybejici implementaci
            //a i tak to routu zvládlo vytvořit a vrátit

            return CreatedAtRoute("GetHighScores", routeValues, _mapper.Map<HighScoreDTO>(highScoreAdded));

        }

        [HttpPatch(("{highScoreId}"))]
        public async Task<ActionResult> PatchHighScore(int highScoreId, JsonPatchDocument<HighScoreWriteDataDTO> patchDocument)
        {
            var highScoreToPatch = (await _highScoreRepository.Find((highscore) => highscore.Id == highScoreId)).FirstOrDefault();

            if (highScoreToPatch == null)
            {
                return NotFound();
            }

            HighScoreWriteDataDTO highScorePatched = _mapper.Map<HighScoreWriteDataDTO>(highScoreToPatch);

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

    }


}
