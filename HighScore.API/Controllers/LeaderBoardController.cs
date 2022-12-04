using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HighScore.API.Controllers
{
    [Route("api/leaderboards")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {

        private IRepository<LeaderBoardEntity> _leaderBoardRepository;
        private IMapper _mapper;

        public LeaderBoardController(IRepository<LeaderBoardEntity> leaderBoardRepository, IMapper mapper)
        {
            _leaderBoardRepository = leaderBoardRepository ?? throw new ArgumentNullException(nameof(leaderBoardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeaderBoardReadDTO>>> GetLeaderBoards(int pageNumber = 1, int pageSize = 10)
        {
            var expression = ExpressionBuilder.CreateExpression<LeaderBoardEntity>((leaderBoard) => 1 == 1);
            var collection = await _leaderBoardRepository.Find(expression);

            if (collection.Count == 0)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<List<LeaderBoardReadDTO>>(collection));

        }

        [HttpPost]
        public async Task<ActionResult> PostHighScore(LeaderBoardWriteDTO leaderBoardData)
        {

            LeaderBoardEntity leaderBoardAdded =
                await _leaderBoardRepository.Add(_mapper.Map<LeaderBoardEntity>(leaderBoardData));

            return Ok();
        }
    }
}
