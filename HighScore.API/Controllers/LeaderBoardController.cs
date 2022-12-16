using AutoMapper;
using HighScore.Data.Repositories;
using HighScore.Domain.Entities;
using HighScore.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HighScore.API.Controllers
{
    [Route("api/leaderboards")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<IEnumerable<LeaderBoardReadDTO>>> GetLeaderBoards(string? sorting, int pageNumber = 1, int pageSize = 10)
        {
            var expression = ExpressionBuilder.CreateExpression<LeaderBoardEntity>((leaderBoard) => 1 == 1);
            var orderPredicate = ExpressionBuilder.CreateExpression<LeaderBoardEntity>((leaderBoard) => leaderBoard.Id);
            if (string.IsNullOrEmpty(sorting))
            {
                sorting = "asc";
            }

            var (collection, paginatonMetaData) = await _leaderBoardRepository.Find(expression, orderPredicate, sorting ,pageNumber, pageSize);

            if (collection.Count == 0)
            {
                return NoContent();
            }

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginatonMetaData));

            return Ok(_mapper.Map<List<LeaderBoardReadDTO>>(collection));

        }

        [HttpPost]
        public async Task<ActionResult> PostLeaderBoard(LeaderBoardWriteDTO leaderBoardData)
        {

            LeaderBoardEntity leaderBoardAdded =
                await _leaderBoardRepository.Add(_mapper.Map<LeaderBoardEntity>(leaderBoardData));

            return Ok();
        }
    }
}
