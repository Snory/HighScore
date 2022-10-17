using HighScore.API.Models;
using System.Linq.Expressions;

namespace HighScore.API.Repositories
{
    public class HighScoreInMemoryRepository : IRepository<HighScoreDTO>
    {
        private List<HighScoreDTO> highScoreDTOs { get; set; }

        public static HighScoreInMemoryRepository Instance { get; } = new HighScoreInMemoryRepository();

        public HighScoreInMemoryRepository()
        {
            highScoreDTOs = new List<HighScoreDTO>()
            {
                new HighScoreDTO() { Id = 1, UserId = 1 , Score = 10 }
            };
        }

        public IEnumerable<HighScoreDTO> Find(Expression<Func<HighScoreDTO, bool>> predicate)
        {
            return highScoreDTOs
                   .AsQueryable()
                   .Where(predicate)
                   .ToList();
        }

        public IEnumerable<HighScoreDTO> GetAll()
        {
            return highScoreDTOs;
        }
    }
}
